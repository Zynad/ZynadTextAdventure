using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/drop-tables")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminDropTablesController(IAdminWorldService adminWorldService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDropTables(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.GetDropTablesAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpsertDropTable(DropTableDto dropTableDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.UpsertDropTableAsync(token, dropTableDto, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{biome}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDropTable(string biome, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await adminWorldService.DeleteDropTableAsync(token, biome, cancellationToken);
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
