using System.Security.Claims;
using System.Text.Encodings.Web;
using ApplicationServices.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TextAdventure.Api.Extensions;

namespace TextAdventure.Api.Authentication;

public class SessionTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;

    public SessionTokenAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        GetCurrentUserHandler getCurrentUserHandler)
        : base(options, logger, encoder, clock)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.GetAccessToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            return AuthenticateResult.NoResult();
        }

        var result = await _getCurrentUserHandler.HandleAsync(token, Context.RequestAborted);
        if (!result.Success || result.User is null)
        {
            return AuthenticateResult.Fail(result.Error ?? "Invalid token");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, result.User.Id.ToString()),
            new(ClaimTypes.Name, result.User.Username)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
