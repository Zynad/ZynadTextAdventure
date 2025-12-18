using Domain.ValueObjects;

namespace ApplicationServices.Characters.Dto;

public record CharacterPresetDto(
    string Id,
    string Name,
    string Description,
    WorldLocation StartingLocation,
    IReadOnlyCollection<InventoryItem> StartingInventory);
