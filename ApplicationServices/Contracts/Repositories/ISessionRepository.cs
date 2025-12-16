using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Repositories;

public interface ISessionRepository
{
    Task AddAsync(SessionToken sessionToken, CancellationToken cancellationToken = default);
    Task<SessionToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<SessionToken>> GetTokensForAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task RemoveExpiredAsync(CancellationToken cancellationToken = default);
}
