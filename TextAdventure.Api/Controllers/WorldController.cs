using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/world")]
public class WorldController(TravelToLocationHandler travelToLocationHandler) : ControllerBase
{
    /// <summary>
    /// Move the authenticated player's character to a new location.
    /// </summary>
    /// <param name="request">Destination details.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("travel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Travel(TravelRequest request, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await travelToLocationHandler.HandleAsync(token, request, cancellationToken);
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

}
