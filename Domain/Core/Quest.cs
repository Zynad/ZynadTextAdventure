using Domain.ValueObjects;

namespace Domain.Core;

public class Quest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? TownName { get; set; }

    public string? AcceptLocationId { get; set; }

    public string? CompletionLocationId { get; set; }

    public List<string> PrerequisiteQuestIds { get; set; } = [];

    public int ExperienceReward { get; set; }
        = 0;

    public int CoinReward { get; set; }
        = 0;

    public List<InventoryItem> RewardItems { get; set; } = [];
}
