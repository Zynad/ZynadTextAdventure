using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/quests")]
public class QuestsController : ControllerBase
{
    private readonly AcceptQuestHandler _acceptQuestHandler;
    private readonly CompleteQuestHandler _completeQuestHandler;

    public QuestsController(
        AcceptQuestHandler acceptQuestHandler,
        CompleteQuestHandler completeQuestHandler)
    {
        _acceptQuestHandler = acceptQuestHandler;
        _completeQuestHandler = completeQuestHandler;
    }

    [HttpPost("{id}/accept")]
    public async Task<IActionResult> Accept(string id, QuestActionRequest request, CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _acceptQuestHandler.HandleAsync(token, id, request, cancellationToken);
        return Translate(result);
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(string id, QuestActionRequest request, CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _completeQuestHandler.HandleAsync(token, id, request, cancellationToken);
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
