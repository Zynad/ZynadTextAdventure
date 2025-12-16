using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/world")]
public class WorldController : ControllerBase
{
    private readonly TravelToLocationHandler _travelToLocationHandler;

    public WorldController(TravelToLocationHandler travelToLocationHandler)
    {
        _travelToLocationHandler = travelToLocationHandler;
    }

    [HttpPost("travel")]
    public async Task<IActionResult> Travel(TravelRequest request, CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _travelToLocationHandler.HandleAsync(token, request, cancellationToken);
        return Translate(result);
    }

    private IActionResult Translate(AdventureResult result)
    {
        if (result.Success && result.Character is not null)
        {
            return Ok(result.Character);
        }

        return result.ErrorType switch
        {
            AdventureErrorType.Unauthorized => Unauthorized(new { message = result.Error ?? "Unauthorized" }),
            AdventureErrorType.NotFound => NotFound(new { message = result.Error ?? "Not found" }),
            AdventureErrorType.Conflict => Conflict(new { message = result.Error ?? "Conflict" }),
            _ => BadRequest(new { message = result.Error ?? "Invalid request" })
        };
    }

    private string ExtractBearerToken()
    {
        if (Request.Headers.TryGetValue("Authorization", out var header)
            && header.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return header.ToString()[7..].Trim();
        }

        return string.Empty;
    }
}
