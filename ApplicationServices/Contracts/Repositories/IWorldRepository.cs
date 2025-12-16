using Domain.Core;

namespace ApplicationServices.Contracts.Repositories;

public interface IWorldRepository
{
    Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default);
    Task SaveWorldAsync(IEnumerable<Town> towns, IEnumerable<Monster> monsters, CancellationToken cancellationToken = default);
}
