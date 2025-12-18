using ApplicationServices.Characters;
using Domain.Core;
using Domain.ValueObjects;
using Shouldly;

namespace TextAdventureTests.Characters;

public class CharacterMapperTests
{
    [Fact]
    public void ToCharacterDto_CopiesInventoryWithoutSharedReferences()
    {
        var character = new Character
        {
            AccountId = Guid.NewGuid(),
            Name = "Hero",
            Inventory = [new() { ItemId = "potion", Quantity = 2 }]
        };

        var dto = CharacterMapper.ToCharacterDto(character);

        character.Inventory.Add(new InventoryItem { ItemId = "elixir", Quantity = 1 });
        character.Inventory[0].Quantity = 5;

        dto.Inventory.ShouldNotBeSameAs(character.Inventory);
        dto.Inventory.Count.ShouldBe(1);
        dto.Inventory.First().Quantity.ShouldBe(2);
    }

    [Fact]
    public void ToPresetDto_ReturnsIndependentInventory()
    {
        var preset = new CharacterPreset
        {
            Id = "warrior",
            Name = "Warrior",
            Description = "Test preset",
            StartingLocation = new WorldLocation { Name = "Start" },
            StartingInventory = [new() { ItemId = "sword", Quantity = 1 }]
        };

        var dto = CharacterMapper.ToPresetDto(preset);

        preset.StartingInventory[0].Quantity = 3;
        dto.StartingInventory.First().Quantity.ShouldBe(1);
    }

    [Fact]
    public void ToStateDto_LimitsHistoryAndMapsFields()
    {
        var encounters = Enumerable.Range(0, 12)
            .Select(i => new Encounter
            {
                EncounterType = "battle",
                OccurredAt = DateTimeOffset.UtcNow.AddMinutes(-i),
                Drops = [new() { ItemId = $"loot_{i}", Quantity = 1 }]
            })
            .ToList();

        var actions = Enumerable.Range(0, 12)
            .Select(i => new CharacterActionLogEntry { Action = $"Action{i}", OccurredAt = DateTimeOffset.UtcNow.AddMinutes(-i) })
            .ToList();

        var character = new Character
        {
            Id = Guid.NewGuid(),
            Name = "Traveler",
            ClassName = "Rogue",
            Stats = new CharacterStats { Combat = 1, Stealth = 2, Pickpocket = 3 },
            Coins = 10,
            Location = new WorldLocation { Name = "Town" },
            Inventory = [new() { ItemId = "coin", Quantity = 5 }],
            QuestStates = [new() { QuestId = "quest1" }],
            EncounterLog = encounters,
            ActionLog = actions
        };

        var dto = CharacterMapper.ToStateDto(character);

        dto.Id.ShouldBe(character.Id);
        dto.ClassName.ShouldBe("Rogue");
        dto.QuestLog.ShouldHaveSingleItem().QuestId.ShouldBe("quest1");
        dto.Encounters.Count.ShouldBe(10);
        dto.Encounters.First().Drops.First().ItemId.ShouldBe("loot_0");
        dto.Actions.Count.ShouldBe(10);
        dto.Actions.First().Action.ShouldBe("Action0");
    }
}
