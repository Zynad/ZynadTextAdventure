using TextAdventure.Api.Models.State;

namespace TextAdventure.Api.Models.Responses;

public class ProgressResponse
{
    public Guid UserId { get; set; }

    public int Level { get; set; }

    public int Experience { get; set; }

    public AdventureState AdventureState { get; set; } = new();

    public DateTimeOffset LastUpdatedUtc { get; set; }
}
