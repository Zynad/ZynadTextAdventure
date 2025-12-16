namespace ApplicationServices.Contracts.Responses;

public class AuthResponse
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = string.Empty;
}
