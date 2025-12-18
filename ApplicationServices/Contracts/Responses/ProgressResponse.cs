using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Responses;

public class ProgressResponse
{
    public Guid UserId { get; set; }

    public int Level { get; set; }

    public int Experience { get; set; }

    public string AdventureState { get; set; } = string.Empty;

    public DateTimeOffset LastUpdatedUtc { get; set; }

    public IReadOnlyCollection<SaveSlot> SaveSlots { get; set; } = [];
}
