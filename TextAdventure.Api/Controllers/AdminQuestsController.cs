using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/quests")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminQuestsController(IAdminQuestService adminQuestService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetQuests(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminQuestService.GetAllAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateQuest(AdminQuestDto questDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminQuestService.CreateAsync(token, questDto, cancellationToken);
        return Translate(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateQuest(string id, AdminQuestDto questDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminQuestService.UpdateAsync(token, id, questDto, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteQuest(string id, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminQuestService.DeleteAsync(token, id, cancellationToken);
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
