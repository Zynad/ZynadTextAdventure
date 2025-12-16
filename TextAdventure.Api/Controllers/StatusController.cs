using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/status")]
public class StatusController : ControllerBase
{
    /// <summary>
    /// Lightweight health check used by deployment targets and local tooling.
    /// </summary>
    /// <returns>Current UTC timestamp and service status.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "ok",
            timestamp = DateTimeOffset.UtcNow
        });
    }
}
