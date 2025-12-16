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

    [HttpGet]
    public async Task<IActionResult> GetMonsters(CancellationToken cancellationToken)
    {
        var monsters = await _gameDataService.GetMonstersAsync(cancellationToken);
        return Ok(monsters);
    }
}
