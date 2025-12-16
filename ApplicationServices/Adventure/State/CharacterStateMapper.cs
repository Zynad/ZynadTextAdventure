using System.Linq;
using Domain.Core;

namespace ApplicationServices.Adventure.State;

public static class CharacterStateMapper
{
    public static CharacterStateDto FromCharacter(Character character)
    {
        var questStates = character.QuestStates
            .Select(q => new QuestStateDto(q.QuestId, q.Status, q.UpdatedAt))
            .ToList();

        var encounterLog = character.EncounterLog ?? new List<Encounter>();

        var encounters = encounterLog
            .OrderByDescending(e => e.OccurredAt)
            .Take(10)
            .Select(e => new EncounterStateDto(
                e.Id,
                e.EncounterType,
                e.Location,
                e.MonsterId,
                e.Outcome,
                e.OccurredAt,
                e.Drops.AsReadOnly()))
            .ToList();

        return new CharacterStateDto(
            character.Id,
            character.Name,
            character.Level,
            character.ClassName,
            character.Location,
            character.Inventory.AsReadOnly(),
            questStates.AsReadOnly(),
            encounters.AsReadOnly());
    }
}
