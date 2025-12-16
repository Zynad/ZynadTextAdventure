namespace Domain.ValueObjects;

public class SessionToken
{
    public string Token { get; set; } = string.Empty;
    public Guid AccountId { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}
