using System.Linq;
using ApplicationServices.Authentication;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Characters.Results;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Characters;

public class CreateCharacterHandler
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IWorldRepository _worldRepository;
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly ILogger<CreateCharacterHandler> _logger;

    public CreateCharacterHandler(
        ICharacterRepository characterRepository,
        IWorldRepository worldRepository,
        GetCurrentUserHandler getCurrentUserHandler,
        ILogger<CreateCharacterHandler> logger)
    {
        _characterRepository = characterRepository;
        _worldRepository = worldRepository;
        _getCurrentUserHandler = getCurrentUserHandler;
        _logger = logger;
    }

    public async Task<CharacterResult> HandleAsync(
        string token,
        CreateCharacterRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var userResult = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return CharacterResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return CharacterResult.Validation("Character name is required");
        }

        var trimmedName = request.Name.Trim();

        if (string.IsNullOrWhiteSpace(request.PresetId))
        {
            return CharacterResult.Validation("PresetId is required");
        }

        var presets = await _worldRepository.GetCharacterPresetsAsync(cancellationToken);
        var preset = presets.FirstOrDefault(p =>
            p.Id.Equals(request.PresetId, StringComparison.OrdinalIgnoreCase));

        if (preset is null)
        {
            return CharacterResult.Validation("Invalid presetId");
        }

        var characters = await _characterRepository.GetByAccountAsync(userResult.User.Id, cancellationToken);
        if (characters.Any(c => c.Name.Equals(trimmedName, StringComparison.OrdinalIgnoreCase)))
        {
            return CharacterResult.Conflict("A character with this name already exists for your account");
        }

        var character = new Character
        {
            AccountId = userResult.User.Id,
            Name = trimmedName,
            PresetId = preset.Id,
            ClassName = preset.Name,
            Location = preset.StartingLocation ?? WorldLocation.Default(),
            Inventory = CopyInventory(preset.StartingInventory)
        };

        await _characterRepository.AddAsync(character, cancellationToken);
        _logger.LogInformation(
            "Created character {CharacterName} for account {AccountId} using preset {PresetId}",
            character.Name,
            character.AccountId,
            preset.Id);

        return CharacterResult.FromSuccess(CharacterMapper.ToCharacterDto(character));
    }

    private static List<InventoryItem> CopyInventory(IEnumerable<InventoryItem> items)
    {
        return items.Select(i => new InventoryItem { ItemId = i.ItemId, Quantity = i.Quantity }).ToList();
    }
}
