namespace ApplicationServices.Authentication.Requests;

public class LoginUserRequest
{
    public string Identifier { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
