using Domain.Core;
using Domain.Entities.Storage;

namespace ApplicationServices.Contracts.Repositories;

public interface IWorldRepository
{
    Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(CancellationToken cancellationToken = default);
    Task SaveWorldAsync(
        IEnumerable<Town> towns,
        IEnumerable<Monster> monsters,
        IEnumerable<CharacterPreset> characterPresets,
        CancellationToken cancellationToken = default);
}
