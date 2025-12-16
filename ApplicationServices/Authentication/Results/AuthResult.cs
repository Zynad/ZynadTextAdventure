using ApplicationServices.Authentication.Models;

namespace ApplicationServices.Authentication.Results;

public class AuthResult
{
    public bool Success { get; init; }
    public AuthErrorType? ErrorType { get; init; }
    public string? Error { get; init; }
    public UserDto? User { get; init; }
    public string? Token { get; init; }

    public static AuthResult Success(UserDto user, string? token = null) => new()
    {
        Success = true,
        User = user,
        Token = token
    };

    public static AuthResult Conflict(string message) => new()
    {
        Success = false,
        ErrorType = AuthErrorType.Conflict,
        Error = message
    };

    public static AuthResult ValidationError(string message) => new()
    {
        Success = false,
        ErrorType = AuthErrorType.Validation,
        Error = message
    };

    public static AuthResult Unauthorized(string message) => new()
    {
        Success = false,
        ErrorType = AuthErrorType.Unauthorized,
        Error = message
    };

    public static AuthResult NotFound(string message) => new()
    {
        Success = false,
        ErrorType = AuthErrorType.NotFound,
        Error = message
    };
}
