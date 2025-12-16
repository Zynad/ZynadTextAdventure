using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventureTests;

internal static class AuthHelpers
{
    public static IOptions<AuthOptions> CreateTestOptions()
    {
        return Options.Create(new AuthOptions
        {
            PasswordPepper = "pepper",
            TokenSecret = "secret",
            TokenExpiryMinutes = 60,
            PasswordIterations = 10_000
        });
    }
}
