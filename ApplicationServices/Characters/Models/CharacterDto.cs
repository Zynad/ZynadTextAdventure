using Domain.Entities.Storage;
using Domain.ValueObjects;

namespace ApplicationServices.Characters.Models;

public record CharacterDto(
    Guid AccountId,
    Guid Id,
    string Name,
    int Level,
    string ClassName,
    string PresetId,
    WorldLocation Location,
    IReadOnlyCollection<InventoryItem> Inventory);
