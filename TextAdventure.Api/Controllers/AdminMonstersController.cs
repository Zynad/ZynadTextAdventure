using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/monsters")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminMonstersController : ControllerBase
{
    private readonly IAdminMonsterService _monsterService;

    public AdminMonstersController(IAdminMonsterService monsterService)
    {
        _monsterService = monsterService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMonsters(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _monsterService.GetAllAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateMonster(MonsterDto monsterDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _monsterService.CreateAsync(token, monsterDto, cancellationToken);
        return Translate(result);
    }

    [HttpPut("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMonster(string name, MonsterDto monsterDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _monsterService.UpdateAsync(token, name, monsterDto, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMonster(string name, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _monsterService.DeleteAsync(token, name, cancellationToken);
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
