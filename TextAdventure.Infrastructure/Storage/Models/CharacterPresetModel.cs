using Domain.ValueObjects;

namespace TextAdventure.Infrastructure.Storage.Models;

public class CharacterPresetModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public WorldLocationModel StartingLocation { get; set; } = new();

    public List<InventoryItem> StartingInventory { get; set; } = new();
}
