using System.Linq;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Contracts.Repositories;

namespace ApplicationServices.Characters;

public class GetCharacterPresetsHandler(IWorldRepository worldRepository)
{
    public async Task<IReadOnlyCollection<CharacterPresetDto>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var presets = await worldRepository.GetCharacterPresetsAsync(cancellationToken);
        return presets
            .Select(CharacterMapper.ToPresetDto)
            .ToList();
    }
}
