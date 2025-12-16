using ApplicationServices.Authentication.Models;
using ApplicationServices.Authentication.Requests;
using ApplicationServices.Authentication.Results;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Authentication;

public class LoginUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly IAuthService _authService;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(IUserRepository userRepository, ISessionRepository sessionRepository, IAuthService authService, ILogger<LoginUserHandler> logger)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _authService = authService;
        _logger = logger;
    }

    public async Task<AuthResult> HandleAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Identifier) || string.IsNullOrWhiteSpace(request.Password))
        {
            return AuthResult.ValidationError("Username/email and password are required");
        }

        var account = await _userRepository.GetByUsernameAsync(request.Identifier, cancellationToken)
                      ?? await _userRepository.GetByEmailAsync(request.Identifier, cancellationToken);

        if (account is null)
        {
            return AuthResult.Unauthorized("Invalid credentials");
        }

        if (!_authService.VerifyPassword(request.Password, account.PasswordHash, account.PasswordSalt))
        {
            return AuthResult.Unauthorized("Invalid credentials");
        }

        var session = _authService.CreateSessionToken(account.Id);
        await _sessionRepository.AddAsync(session, cancellationToken);
        _logger.LogInformation("Issued session for {Username}", account.Username);

        return AuthResult.FromSuccess(new UserDto(account.Id, account.Username, account.Email), session.Token);
    }
}
