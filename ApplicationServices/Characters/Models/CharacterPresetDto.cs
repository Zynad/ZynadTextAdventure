using Domain.Entities.Storage;
using Domain.ValueObjects;

namespace ApplicationServices.Characters.Models;

public record CharacterPresetDto(
    string Id,
    string Name,
    string Description,
    WorldLocation StartingLocation,
    IReadOnlyCollection<InventoryItem> StartingInventory);
