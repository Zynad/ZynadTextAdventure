using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/quests")]
public class QuestsController(
    AcceptQuestHandler acceptQuestHandler,
    CompleteQuestHandler completeQuestHandler)
    : ControllerBase
{
    /// <summary>
    /// Accept a quest for the current player's character.
    /// </summary>
    /// <param name="id">Quest identifier.</param>
    /// <param name="request">Action details including the target character.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("{id}/accept")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Accept(string id, QuestActionRequest request, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await acceptQuestHandler.HandleAsync(token, id, request, cancellationToken);
        return Translate(result);
    }

    /// <summary>
    /// Complete a quest for the current player's character.
    /// </summary>
    /// <param name="id">Quest identifier.</param>
    /// <param name="request">Action details including the target character.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Complete(string id, QuestActionRequest request, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await completeQuestHandler.HandleAsync(token, id, request, cancellationToken);
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
