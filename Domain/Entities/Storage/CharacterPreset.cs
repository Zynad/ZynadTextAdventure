using Domain.ValueObjects;

namespace Domain.Entities.Storage;

public class CharacterPreset
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public WorldLocation StartingLocation { get; set; } = WorldLocation.Default();

    public List<InventoryItem> StartingInventory { get; set; } = new();
}
