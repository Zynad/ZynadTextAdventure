namespace Domain.ValueObjects;

public class PasswordHash
{
    public string Hash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}
