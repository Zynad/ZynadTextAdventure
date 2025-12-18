using ApplicationServices.Admin;
using ApplicationServices.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/admin/items")]
[Authorize(Policy = AuthPolicies.AuthenticatedUsers)]
public class AdminItemsController : ControllerBase
{
    private readonly IAdminItemService _itemService;

    public AdminItemsController(IAdminItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetItems(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _itemService.GetAllAsync(token, cancellationToken);
        return Translate(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateItem(ItemDto itemDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _itemService.CreateAsync(token, itemDto, cancellationToken);
        return Translate(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItem(Guid id, ItemDto itemDto, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var payload = itemDto with { Id = id };
        var result = await _itemService.UpdateAsync(token, payload, cancellationToken);
        return Translate(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItem(Guid id, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _itemService.DeleteAsync(token, id, cancellationToken);
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
