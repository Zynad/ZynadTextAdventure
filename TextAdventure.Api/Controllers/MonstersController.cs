using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Services;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MonstersController : ControllerBase
{
    private readonly IGameDataService _gameDataService;

    public MonstersController(IGameDataService gameDataService)
    {
        _gameDataService = gameDataService;
    }

    /// <summary>
    /// Retrieve all monster profiles.
    /// </summary>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonsters(CancellationToken cancellationToken)
    {
        var monsters = await _gameDataService.GetMonstersAsync(cancellationToken);
        return Ok(monsters);
    }
}
