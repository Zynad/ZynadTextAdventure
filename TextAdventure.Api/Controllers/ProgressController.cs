using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Contracts.Requests;
using ApplicationServices.Services;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly IGameDataService _gameDataService;

    public ProgressController(IGameDataService gameDataService)
    {
        _gameDataService = gameDataService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest(new { message = "A session token is required" });
        }

        var progress = await _gameDataService.GetProgressAsync(token, cancellationToken);
        if (progress is null)
        {
            return NotFound(new { message = "No saved progress for this session" });
        }

        return Ok(progress);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(SaveProgressRequest request, CancellationToken cancellationToken)
    {
        var success = await _gameDataService.SaveProgressAsync(request, cancellationToken);
        if (!success)
        {
            return Unauthorized(new { message = "Invalid session token" });
        }

        return Ok(new { message = "Progress saved" });
    }
}
