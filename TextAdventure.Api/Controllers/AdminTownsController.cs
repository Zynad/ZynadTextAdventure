using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/towns")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminTownsController(IAdminWorldService adminWorldService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTowns(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.GetTownsAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateTown(TownDto townDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.CreateTownAsync(token, townDto, cancellationToken);
        return Translate(result);
    }

    [HttpPut("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateTown(string name, TownDto townDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.UpdateTownAsync(token, name, townDto, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTown(string name, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.DeleteTownAsync(token, name, cancellationToken);
        return Translate(result);
    }

    [HttpPost("{townName}/npcs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateNpc(string townName, TownNpcDto townNpcDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.CreateNpcAsync(token, townName, townNpcDto, cancellationToken);
        return Translate(result);
    }

    [AcceptVerbs("PUT", Route = "{townName}/npcs/{npcId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateNpc(
        string townName,
        string npcId,
        TownNpcDto townNpcDto,
        CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.UpdateNpcAsync(token, townName, npcId, townNpcDto, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{townName}/npcs/{npcId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNpc(string townName, string npcId, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.DeleteNpcAsync(token, townName, npcId, cancellationToken);
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
