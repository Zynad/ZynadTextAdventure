namespace TextAdventure.Infrastructure.Configuration;

public class AuthOptions
{
    public string TokenSecret { get; set; } = string.Empty;
    public string PasswordPepper { get; set; } = string.Empty;
    public int TokenExpiryMinutes { get; set; } = 60;
    public int PasswordIterations { get; set; } = 100_000;
}
