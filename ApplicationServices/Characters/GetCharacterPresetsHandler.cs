using ApplicationServices.Characters.Models;
using ApplicationServices.Contracts.Repositories;
using Domain.ValueObjects;

namespace ApplicationServices.Characters;

public class GetCharacterPresetsHandler
{
    private readonly IWorldRepository _worldRepository;

    public GetCharacterPresetsHandler(IWorldRepository worldRepository)
    {
        _worldRepository = worldRepository;
    }

    public async Task<IReadOnlyCollection<CharacterPresetDto>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var presets = await _worldRepository.GetCharacterPresetsAsync(cancellationToken);
        return presets
            .Select(p => new CharacterPresetDto(
                p.Id,
                p.Name,
                p.Description,
                p.StartingLocation,
                p.StartingInventory
                    .Select(i => new InventoryItem { ItemId = i.ItemId, Quantity = i.Quantity })
                    .ToList()))
            .ToList();
    }
}
