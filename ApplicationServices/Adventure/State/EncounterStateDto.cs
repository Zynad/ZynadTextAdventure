using Domain.ValueObjects;

namespace ApplicationServices.Adventure.State;

public record EncounterStateDto(
    string Id,
    string EncounterType,
    string Location,
    string MonsterId,
    string Outcome,
    DateTimeOffset OccurredAt,
    IReadOnlyCollection<InventoryItem> Drops);
