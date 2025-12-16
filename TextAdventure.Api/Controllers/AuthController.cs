using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Contracts.Requests;
using ApplicationServices.Services;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IGameDataService _gameDataService;

    public AuthController(IGameDataService gameDataService)
    {
        _gameDataService = gameDataService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _gameDataService.RegisterAsync(request, cancellationToken);
        if (result is null)
        {
            return Conflict(new { message = "Username already exists" });
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _gameDataService.LoginAsync(request, cancellationToken);
        if (result is null)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        return Ok(result);
    }
}
