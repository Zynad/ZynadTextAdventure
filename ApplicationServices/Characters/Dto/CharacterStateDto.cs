using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Characters.Dto;

public record CharacterStateDto(
    Guid Id,
    string Name,
    int Level,
    string ClassName,
    CharacterStats Stats,
    decimal Coins,
    WorldLocation Location,
    IReadOnlyCollection<InventoryItem> Inventory,
    IReadOnlyCollection<QuestStateDto> QuestLog,
    IReadOnlyCollection<EncounterStateDto> Encounters,
    IReadOnlyCollection<CharacterActionLogEntry> Actions);
