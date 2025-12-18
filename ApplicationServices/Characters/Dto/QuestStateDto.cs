using Domain.Core;

namespace ApplicationServices.Characters.Dto;

public record QuestStateDto(
    string QuestId,
    QuestProgressStatus Status,
    DateTimeOffset UpdatedAt);
