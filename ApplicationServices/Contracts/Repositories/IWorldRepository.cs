using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Repositories;

public interface IWorldRepository
{
    Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<WorldLocationNode>> GetLocationsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<DropTable>> GetDropTablesAsync(CancellationToken cancellationToken = default);
    Task SaveWorldAsync(
        IEnumerable<Town> towns,
        IEnumerable<Monster> monsters,
        IEnumerable<CharacterPreset> characterPresets,
        IEnumerable<WorldLocationNode> locations,
        IEnumerable<DropTable> dropTables,
        CancellationToken cancellationToken = default);
}
