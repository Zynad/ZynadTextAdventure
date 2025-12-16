using ApplicationServices.Authentication.Models;
using ApplicationServices.Authentication.Results;
using ApplicationServices.Contracts.Repositories;

namespace ApplicationServices.Authentication;

public class GetCurrentUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;

    public GetCurrentUserHandler(IUserRepository userRepository, ISessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<AuthResult> HandleAsync(string token, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return AuthResult.Unauthorized("Missing token");
        }

        var session = await _sessionRepository.GetValidTokenAsync(token, cancellationToken);
        if (session is null)
        {
            return AuthResult.Unauthorized("Invalid or expired token");
        }

        var account = await _userRepository.GetByIdAsync(session.AccountId, cancellationToken);
        if (account is null)
        {
            return AuthResult.NotFound("Account not found");
        }

        return AuthResult.Success(new UserDto(account.Id, account.Username, account.Email));
    }
}
