using System.Collections.Generic;
using System.Linq;
using ApplicationServices.Characters.Dto;
using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Characters;

public static class CharacterMapper
{
    public static CharacterDto ToCharacterDto(Character character)
    {
        return new CharacterDto(
            character.AccountId,
            character.Id,
            character.Name,
            character.Level,
            character.Stats,
            character.Coins,
            character.ClassName,
            character.PresetId,
            character.Location,
            CopyInventory(character.Inventory));
    }

    public static CharacterPresetDto ToPresetDto(CharacterPreset preset)
    {
        return new CharacterPresetDto(
            preset.Id,
            preset.Name,
            preset.Description,
            preset.StartingLocation,
            CopyInventory(preset.StartingInventory));
    }

    public static CharacterStateDto ToStateDto(Character character)
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
                CopyInventory(e.Drops)))
            .ToList();

        var actions = (character.ActionLog ?? new List<CharacterActionLogEntry>())
            .OrderByDescending(a => a.OccurredAt)
            .Take(10)
            .ToList();

        return new CharacterStateDto(
            character.Id,
            character.Name,
            character.Level,
            character.ClassName,
            character.Stats,
            character.Coins,
            character.Location,
            CopyInventory(character.Inventory),
            questStates.AsReadOnly(),
            encounters.AsReadOnly(),
            actions.AsReadOnly());
    }

    private static IReadOnlyCollection<InventoryItem> CopyInventory(IEnumerable<InventoryItem> items)
    {
        return items
            .Select(i => new InventoryItem { ItemId = i.ItemId, Quantity = i.Quantity })
            .ToList()
            .AsReadOnly();
    }
}
