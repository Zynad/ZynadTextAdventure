using TextAdventure.Api.Models.State;

namespace TextAdventure.Api.Models;

public class PlayerProgress
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public int Level { get; set; }

    public int Experience { get; set; }

    public AdventureState AdventureState { get; set; } = new();

    public DateTimeOffset LastUpdatedUtc { get; set; } = DateTimeOffset.UtcNow;
}
