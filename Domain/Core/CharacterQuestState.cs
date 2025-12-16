namespace Domain.Core;

public class CharacterQuestState
{
    public string QuestId { get; set; } = string.Empty;

    public QuestProgressStatus Status { get; set; } = QuestProgressStatus.Accepted;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
