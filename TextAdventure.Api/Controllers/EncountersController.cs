using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Results;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/encounters")]
public class EncountersController : ControllerBase
{
    private readonly GetEncountersHandler _getEncountersHandler;

    public EncountersController(GetEncountersHandler getEncountersHandler)
    {
        _getEncountersHandler = getEncountersHandler;
    }

    [HttpGet("{characterId:guid}")]
    public async Task<IActionResult> Get(Guid characterId, CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _getEncountersHandler.HandleAsync(token, characterId, cancellationToken);

        if (result.Success && result.Character is not null)
        {
            return Ok(new
            {
                characterId = result.Character.Id,
                encounters = result.Character.Encounters
            });
        }

        return Translate(result);
    }

    private IActionResult Translate(AdventureResult result)
    {
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
