using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Authentication;
using ApplicationServices.Authentication.Requests;
using ApplicationServices.Authentication.Results;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly LoginUserHandler _loginUserHandler;
    private readonly GetCurrentUserHandler _getCurrentUserHandler;

    public AuthController(RegisterUserHandler registerUserHandler, LoginUserHandler loginUserHandler, GetCurrentUserHandler getCurrentUserHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
        _getCurrentUserHandler = getCurrentUserHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _registerUserHandler.HandleAsync(request, cancellationToken);
        return TranslateResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _loginUserHandler.HandleAsync(request, cancellationToken);
        return TranslateResult(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!result.Success)
        {
            return result.ErrorType == AuthErrorType.NotFound
                ? NotFound(new { message = result.Error })
                : Unauthorized(new { message = result.Error });
        }

        return Ok(result.User);
    }

    private IActionResult TranslateResult(AuthResult result)
    {
        if (result.Success)
        {
            return Ok(new { user = result.User, token = result.Token });
        }

        return result.ErrorType switch
        {
            AuthErrorType.Conflict => Conflict(new { message = result.Error }),
            AuthErrorType.Unauthorized => Unauthorized(new { message = result.Error }),
            AuthErrorType.Validation => BadRequest(new { message = result.Error }),
            _ => BadRequest(new { message = result.Error })
        };
    }

    private string ExtractBearerToken()
    {
        if (Request.Headers.TryGetValue("Authorization", out var header) && header.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return header.ToString()[7..].Trim();
        }

        return string.Empty;
    }
}
