using Domain.Core;

namespace ApplicationServices.Contracts.Repositories;

public interface IQuestRepository
{
    Task<Quest?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Quest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Quest quest, CancellationToken cancellationToken = default);
    Task UpdateAsync(Quest quest, CancellationToken cancellationToken = default);
}
