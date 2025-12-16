using Domain.Core;

namespace ApplicationServices.Adventure.State;

public record QuestStateDto(
    string QuestId,
    QuestProgressStatus Status,
    DateTimeOffset UpdatedAt);
