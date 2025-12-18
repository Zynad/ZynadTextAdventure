using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Authentication;
using ApplicationServices.Authentication.Requests;
using ApplicationServices.Authentication.Results;
using TextAdventure.Api.Authentication;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    RegisterUserHandler registerUserHandler,
    LoginUserHandler loginUserHandler,
    GetCurrentUserHandler getCurrentUserHandler)
    : ControllerBase
{
    /// <summary>
    /// Register a new user and create a session token.
    /// </summary>
    /// <param name="request">The registration payload.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>The newly created user and session token.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var result = await registerUserHandler.HandleAsync(request, cancellationToken);
        return TranslateResult(result);
    }

    /// <summary>
    /// Exchange credentials for a session token.
    /// </summary>
    /// <param name="request">Login credentials.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>The authenticated user and session token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await loginUserHandler.HandleAsync(request, cancellationToken);
        return TranslateResult(result);
    }

    /// <summary>
    /// Resolve the current user using the provided bearer token or auth cookie.
    /// </summary>
    /// <param name="cancellationToken">Request cancellation token.</param>
    /// <returns>The current user details.</returns>
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
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
            if (!string.IsNullOrWhiteSpace(result.Token))
            {
                Response.Cookies.Append(AuthConstants.AuthCookieName, result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = Request.IsHttps,
                    SameSite = SameSiteMode.Lax,
                    Path = "/"
                });
            }

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

}
