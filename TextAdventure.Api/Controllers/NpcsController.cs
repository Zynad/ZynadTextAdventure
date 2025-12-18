using ApplicationServices.Npc;
using ApplicationServices.Npc.Models;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/npcs")]
public class NpcsController(NpcInteractionService npcInteractionService) : ControllerBase
{
    /// <summary>
    /// Get a dialogue line from the given NPC, including player-name interpolation.
    /// </summary>
    /// <param name="npcId">The NPC identifier.</param>
    /// <param name="characterId">The player character identifier.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("{npcId}/dialogue")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDialogue(
        string npcId,
        [FromQuery] Guid characterId,
        CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await npcInteractionService.GetDialogueAsync(token, characterId, npcId, cancellationToken);
        return Translate(result);
    }

    /// <summary>
    /// Request a quest prompt from the specified NPC.
    /// </summary>
    /// <param name="npcId">The NPC identifier.</param>
    /// <param name="characterId">The player character identifier.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("{npcId}/quests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> OfferQuest(
        string npcId,
        [FromQuery] Guid characterId,
        CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await npcInteractionService.OfferQuestAsync(token, characterId, npcId, cancellationToken);
        return Translate(result);
    }

    /// <summary>
    /// Perform a buy or sell transaction with an NPC vendor.
    /// </summary>
    /// <param name="npcId">The NPC vendor identifier.</param>
    /// <param name="request">Trade request payload.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("{npcId}/trade")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Trade(
        string npcId,
        [FromBody] TradeRequest request,
        CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await npcInteractionService.TradeAsync(
            token,
            request.CharacterId,
            npcId,
            request.ItemId,
            request.Quantity,
            request.Action,
            cancellationToken);

        return Translate(result);
    }

    /// <summary>
    /// Resolve a stat-based action (combat, stealth, pickpocket) against the NPC.
    /// </summary>
    /// <param name="npcId">The NPC identifier.</param>
    /// <param name="request">Action request payload.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("{npcId}/actions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResolveAction(
        string npcId,
        [FromBody] ActionRequest request,
        CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await npcInteractionService.ResolveActionAsync(
            token,
            request.CharacterId,
            npcId,
            request.Action,
            request.Difficulty,
            cancellationToken);

        return Translate(result);
    }

    private IActionResult Translate<T>(NpcInteractionResult<T> result)
    {
        if (result.Success && result.Payload is not null)
        {
            return Ok(new
            {
                data = result.Payload,
                character = result.Character
            });
        }

        return BadRequest(new { message = result.Error ?? "Unknown error" });
    }
}

public record TradeRequest(Guid CharacterId, string ItemId, int Quantity, TradeAction Action);

public record ActionRequest(Guid CharacterId, NpcActionType Action, int Difficulty);
