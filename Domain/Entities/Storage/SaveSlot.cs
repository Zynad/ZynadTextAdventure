namespace Domain.Entities.Storage;

public class SaveSlot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "Slot 1";

    public int Level { get; set; }

    public int Experience { get; set; }

    public string AdventureState { get; set; } = string.Empty;

    public DateTimeOffset LastUpdatedUtc { get; set; } = DateTimeOffset.UtcNow;

    public WorldLocation Location { get; set; } = WorldLocation.Default();
}
