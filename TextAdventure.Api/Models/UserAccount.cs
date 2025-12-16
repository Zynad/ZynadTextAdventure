using System.Text.Json.Serialization;

namespace TextAdventure.Api.Models;

public class UserAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Username { get; set; } = string.Empty;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public List<string> SessionTokens { get; set; } = new();
}
