using System.Linq;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Contracts.Repositories;

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
            .Select(CharacterMapper.ToPresetDto)
            .ToList();
    }
}
