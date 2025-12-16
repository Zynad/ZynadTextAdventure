using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Services;

public interface IAuthService
{
    Task<Account?> RegisterAsync(string username, string password, CancellationToken cancellationToken = default);
    Task<SessionToken?> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
    Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
}
