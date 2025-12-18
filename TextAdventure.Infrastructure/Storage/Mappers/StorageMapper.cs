using Domain.Core;
using Domain.ValueObjects;
using TextAdventure.Infrastructure.Storage.Models;

namespace TextAdventure.Infrastructure.Storage.Mappers;

public static class StorageMapper
{
    public static WorldState ToDomain(this WorldStateModel model)
    {
        return new WorldState
        {
            Towns = model.Towns,
            Monsters = model.Monsters,
            CharacterPresets = model.CharacterPresets.Select(p => p.ToDomain()).ToList(),
            Locations = model.Locations.Select(l => l.ToDomain()).ToList(),
            DropTables = model.DropTables
        };
    }

    public static WorldStateModel ToModel(this WorldState state)
    {
        return new WorldStateModel
        {
            Towns = state.Towns,
            Monsters = state.Monsters,
            CharacterPresets = state.CharacterPresets.Select(p => p.ToModel()).ToList(),
            Locations = state.Locations.Select(l => l.ToModel()).ToList(),
            DropTables = state.DropTables
        };
    }

    public static Character ToDomain(this CharacterModel model)
    {
        return new Character
        {
            Id = model.Id,
            AccountId = model.AccountId,
            Name = model.Name,
            PresetId = model.PresetId,
            ClassName = model.ClassName,
            Level = model.Level,
            Coins = model.Coins,
            Stats = model.Stats,
            Location = model.Location.ToDomain(),
            Inventory = model.Inventory,
            QuestStates = model.QuestStates,
            EncounterLog = model.EncounterLog,
            ActionLog = model.ActionLog
        };
    }

    public static CharacterModel ToModel(this Character entity)
    {
        return new CharacterModel
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            Name = entity.Name,
            PresetId = entity.PresetId,
            ClassName = entity.ClassName,
            Level = entity.Level,
            Coins = entity.Coins,
            Stats = entity.Stats,
            Location = entity.Location.ToModel(),
            Inventory = entity.Inventory,
            QuestStates = entity.QuestStates,
            EncounterLog = entity.EncounterLog,
            ActionLog = entity.ActionLog
        };
    }

    public static WorldLocation ToDomain(this WorldLocationModel model)
    {
        return new WorldLocation
        {
            Name = model.Name,
            Biome = model.Biome,
            ThreatLevel = model.ThreatLevel
        };
    }

    public static WorldLocationModel ToModel(this WorldLocation value)
    {
        return new WorldLocationModel
        {
            Name = value.Name,
            Biome = value.Biome,
            ThreatLevel = value.ThreatLevel
        };
    }

    public static WorldLocationNode ToDomain(this WorldLocationNodeModel model)
    {
        return new WorldLocationNode
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Biome = model.Biome,
            ThreatLevel = model.ThreatLevel,
            AdjacentLocationIds = model.AdjacentLocationIds,
            TownName = model.TownName
        };
    }

    public static WorldLocationNodeModel ToModel(this WorldLocationNode node)
    {
        return new WorldLocationNodeModel
        {
            Id = node.Id,
            Name = node.Name,
            Description = node.Description,
            Biome = node.Biome,
            ThreatLevel = node.ThreatLevel,
            AdjacentLocationIds = node.AdjacentLocationIds,
            TownName = node.TownName
        };
    }

    public static CharacterPreset ToDomain(this CharacterPresetModel model)
    {
        return new CharacterPreset
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            StartingLocation = model.StartingLocation.ToDomain(),
            StartingInventory = model.StartingInventory
        };
    }

    public static CharacterPresetModel ToModel(this CharacterPreset preset)
    {
        return new CharacterPresetModel
        {
            Id = preset.Id,
            Name = preset.Name,
            Description = preset.Description,
            StartingLocation = preset.StartingLocation.ToModel(),
            StartingInventory = preset.StartingInventory
        };
    }
}
