using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Contracts.Requests;
using ApplicationServices.Services;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController(IGameDataService gameDataService) : ControllerBase
{
    /// <summary>
    /// Load save progress using a query token, bearer header, or auth cookie.
    /// </summary>
    /// <param name="token">Optional session token passed via query string.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] string? token, CancellationToken cancellationToken)
    {
        var accessToken = !string.IsNullOrWhiteSpace(token)
            ? token!
            : Request.GetAccessToken();

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return BadRequest(new { message = "A session token is required" });
        }

        var progress = await gameDataService.GetProgressAsync(accessToken, cancellationToken);
        if (progress is null)
        {
            return NotFound(new { message = "No saved progress for this session" });
        }

        return Ok(progress);
    }

    /// <summary>
    /// Persist progress for the supplied session token.
    /// </summary>
    /// <param name="request">The save payload including the session token.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("save")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Save(SaveProgressRequest request, CancellationToken cancellationToken)
    {
        var success = await gameDataService.SaveProgressAsync(request, cancellationToken);
        if (!success)
        {
            return Unauthorized(new { message = "Invalid session token" });
        }

        return Ok(new { message = "Progress saved" });
    }
}
