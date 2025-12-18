using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/weapons")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminWeaponsController : ControllerBase
{
    private readonly IAdminWeaponService _weaponService;

    public AdminWeaponsController(IAdminWeaponService weaponService)
    {
        _weaponService = weaponService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWeapons(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _weaponService.GetAllAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateWeapon(WeaponDto weaponDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _weaponService.CreateAsync(token, weaponDto, cancellationToken);
        return Translate(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWeapon(Guid id, WeaponDto weaponDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var payload = weaponDto with { Id = id };
        var result = await _weaponService.UpdateAsync(token, payload, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWeapon(Guid id, [FromQuery] WeaponTypeEntity weaponType, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _weaponService.DeleteAsync(token, id, weaponType, cancellationToken);
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
