using Domain.Entities.Storage;
using Domain.ValueObjects;

namespace ApplicationServices.Adventure.State;

public record CharacterStateDto(
    Guid Id,
    string Name,
    int Level,
    string ClassName,
    WorldLocation Location,
    IReadOnlyCollection<InventoryItem> Inventory,
    IReadOnlyCollection<QuestStateDto> QuestLog,
    IReadOnlyCollection<EncounterStateDto> Encounters);
