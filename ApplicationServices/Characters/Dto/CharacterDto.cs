using Domain.ValueObjects;

namespace ApplicationServices.Characters.Dto;

public record CharacterDto(
    Guid AccountId,
    Guid Id,
    string Name,
    int Level,
    CharacterStats Stats,
    decimal Coins,
    string ClassName,
    string PresetId,
    WorldLocation Location,
    IReadOnlyCollection<InventoryItem> Inventory);
