namespace ApplicationServices.Admin.Models;

public record AdminQuestDto(
    string Id,
    string Name,
    string Description,
    string? TownName,
    string? AcceptLocationId,
    string? CompletionLocationId,
    IReadOnlyCollection<string> PrerequisiteQuestIds,
    int ExperienceReward,
    int CoinReward,
    IReadOnlyCollection<QuestRewardItemDto> RewardItems);
