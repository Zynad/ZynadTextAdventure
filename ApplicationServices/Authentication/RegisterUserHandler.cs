using System.ComponentModel.DataAnnotations;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using ApplicationServices.Authentication.Results;
using ApplicationServices.Authentication.Requests;
using ApplicationServices.Authentication.Models;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Authentication;

public class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly IAuthService _authService;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(IUserRepository userRepository, ISessionRepository sessionRepository, IAuthService authService, ILogger<RegisterUserHandler> logger)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _authService = authService;
        _logger = logger;
    }

    public async Task<AuthResult> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var validationError = Validate(request);
        if (validationError is not null)
        {
            return AuthResult.ValidationError(validationError);
        }

        var existingUsername = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (existingUsername is not null)
        {
            return AuthResult.Conflict("Username already exists");
        }

        var existingEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingEmail is not null)
        {
            return AuthResult.Conflict("Email already exists");
        }

        var passwordHash = _authService.HashPassword(request.Password);

        var account = new Domain.Core.Account
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash.Hash,
            PasswordSalt = passwordHash.Salt
        };

        await _userRepository.AddAsync(account, cancellationToken);

        var session = _authService.CreateSessionToken(account.Id);
        await _sessionRepository.AddAsync(session, cancellationToken);

        _logger.LogInformation("Registered new user {Username}", account.Username);

        return AuthResult.Success(new UserDto(account.Id, account.Username, account.Email), session.Token);
    }

    private static string? Validate(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < 3)
        {
            return "Username must be at least 3 characters";
        }

        if (string.IsNullOrWhiteSpace(request.Email) || !new EmailAddressAttribute().IsValid(request.Email))
        {
            return "A valid email address is required";
        }

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
        {
            return "Password must be at least 8 characters";
        }

        if (!request.Password.Any(char.IsUpper) || !request.Password.Any(char.IsLower) || !request.Password.Any(char.IsDigit))
        {
            return "Password must contain upper, lower, and numeric characters";
        }

        return null;
    }
}
