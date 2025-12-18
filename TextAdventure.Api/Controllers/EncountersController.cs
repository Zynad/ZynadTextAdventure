using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/encounters")]
public class EncountersController(GetEncountersHandler getEncountersHandler) : ControllerBase
{
    /// <summary>
    /// Get the encounters available for a character.
    /// </summary>
    /// <param name="characterId">The character identifier.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("{characterId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Get(Guid characterId, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await getEncountersHandler.HandleAsync(token, characterId, cancellationToken);

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

}
