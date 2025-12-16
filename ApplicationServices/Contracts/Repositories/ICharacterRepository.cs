using Domain.Core;

namespace ApplicationServices.Contracts.Repositories;

public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Character>> GetByAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Character>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Character character, CancellationToken cancellationToken = default);
    Task UpdateAsync(Character character, CancellationToken cancellationToken = default);
}
