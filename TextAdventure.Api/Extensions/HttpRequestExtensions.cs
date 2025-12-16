using Microsoft.AspNetCore.Http;
using TextAdventure.Api.Authentication;

namespace TextAdventure.Api.Extensions;

public static class HttpRequestExtensions
{
    public static string GetAccessToken(this HttpRequest request)
    {
        if (request.Headers.TryGetValue(AuthConstants.AuthorizationHeaderName, out var header)
            && header.ToString().StartsWith(AuthConstants.BearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return header.ToString()[AuthConstants.BearerPrefix.Length..].Trim();
        }

        if (request.Cookies.TryGetValue(AuthConstants.AuthCookieName, out var cookieToken)
            && !string.IsNullOrWhiteSpace(cookieToken))
        {
            return cookieToken;
        }

        return string.Empty;
    }
}
