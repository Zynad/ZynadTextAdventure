namespace Domain.ValueObjects;

public class PlayerProgress
{
    public Guid UserId { get; set; }

    public int Level { get; set; }

    public int Experience { get; set; }

    public string AdventureState { get; set; } = string.Empty;

    public DateTimeOffset LastUpdatedUtc { get; set; }

    public List<SaveSlot> SaveSlots { get; set; } = new();
}
