using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Services;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MonstersController : ControllerBase
{
    private readonly GameDataService _gameDataService;

    public MonstersController(GameDataService gameDataService)
    {
        _gameDataService = gameDataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMonsters(CancellationToken cancellationToken)
    {
        var monsters = await _gameDataService.GetMonstersAsync(cancellationToken);
        return Ok(monsters);
    }
}
