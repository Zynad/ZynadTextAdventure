using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/armor")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminArmorController(IAdminArmorService adminArmorService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetArmor(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminArmorService.GetAllAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost("{slot}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateArmor(string slot, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<ArmorSlot>(slot, true, out var parsedSlot))
        {
            return BadRequest(new { message = "Invalid armor slot" });
        }

        var token = Request.GetAccessToken();
        var payload = armorPieceDto with { Slot = parsedSlot };
        var result = await adminArmorService.CreateAsync(token, payload, cancellationToken);
        return Translate(result);
    }

    [HttpPut("{slot}/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateArmor(string slot, Guid id, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<ArmorSlot>(slot, true, out var parsedSlot))
        {
            return BadRequest(new { message = "Invalid armor slot" });
        }

        var token = Request.GetAccessToken();
        var payload = armorPieceDto with { Id = id, Slot = parsedSlot };
        var result = await adminArmorService.UpdateAsync(token, payload, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{slot}/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteArmor(string slot, Guid id, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<ArmorSlot>(slot, true, out var parsedSlot))
        {
            return BadRequest(new { message = "Invalid armor slot" });
        }

        var token = Request.GetAccessToken();
        var result = await adminArmorService.DeleteAsync(token, id, parsedSlot, cancellationToken);
        return Translate(result);
    }

    private IActionResult Translate<T>(AdminOperationResult<T> result)
    {
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return result.ErrorType switch
        {
            AdminErrorType.Validation => BadRequest(new { message = result.Error }),
            AdminErrorType.NotFound => NotFound(new { message = result.Error }),
            AdminErrorType.Conflict => Conflict(new { message = result.Error }),
            _ => Unauthorized(new { message = result.Error ?? "Unauthorized" })
        };
    }
}
