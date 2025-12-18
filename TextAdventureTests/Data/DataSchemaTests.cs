using System.Text.Json;
using Domain.Core;
using Domain.ValueObjects;
using Shouldly;

namespace TextAdventureTests.Data;

public class DataSchemaTests
{
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    [Fact]
    public void WorldData_ShouldDefineConnectedGraphAndDropTables()
    {
        var world = ReadWorld();

        world.Locations.Count.ShouldBeGreaterThan(10);
        world.Towns.Count.ShouldBeGreaterThan(1);
        world.DropTables.Count.ShouldBeGreaterThan(1);

        var towns = world.Towns.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var locationDictionary = world.Locations.ToDictionary(l => l.Id, StringComparer.OrdinalIgnoreCase);
        var uniqueLocationIds = new HashSet<string>(locationDictionary.Keys, StringComparer.OrdinalIgnoreCase);
        uniqueLocationIds.Count.ShouldBe(world.Locations.Count);

        foreach (var town in world.Towns)
        {
            town.Npcs.ShouldNotBeNull();
            town.Npcs.Count.ShouldBeGreaterThanOrEqualTo(2);
            town.Npcs.ShouldAllBe(n => !string.IsNullOrWhiteSpace(n.Name) && !string.IsNullOrWhiteSpace(n.Role));
        }

        foreach (var location in world.Locations)
        {
            if (!string.IsNullOrWhiteSpace(location.TownName))
            {
                towns.ShouldContain(location.TownName);
            }

            foreach (var adjacentId in location.AdjacentLocationIds)
            {
                locationDictionary.ShouldContainKey(adjacentId);
                locationDictionary[adjacentId].AdjacentLocationIds.ShouldContain(location.Id);
            }
        }

        var dropTablesByBiome = world.DropTables.ToDictionary(d => d.Biome, StringComparer.OrdinalIgnoreCase);
        var usedBiomes = world.Locations.Select(l => l.Biome).Where(b => !string.IsNullOrWhiteSpace(b)).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var biome in usedBiomes)
        {
            dropTablesByBiome.ShouldContainKey(biome);
            dropTablesByBiome[biome].Drops.ShouldAllBe(d => !string.IsNullOrWhiteSpace(d));
            dropTablesByBiome[biome].Drops.Count.ShouldBeGreaterThan(0);
        }

        foreach (var monster in world.Monsters)
        {
            monster.Id.ShouldNotBeNullOrWhiteSpace();
            monster.Name.ShouldNotBeNullOrWhiteSpace();
            monster.Biome.ShouldNotBeNullOrWhiteSpace();
            dropTablesByBiome.ShouldContainKey(monster.Biome);

            AssertRange(monster.LevelRange, nameof(monster.LevelRange));
            AssertRange(monster.HitPointRange, nameof(monster.HitPointRange));
            AssertRange(monster.AttackRange, nameof(monster.AttackRange));
            AssertRange(monster.DefenseRange, nameof(monster.DefenseRange), allowZero: true);
            AssertRange(monster.CoinDropRange, nameof(monster.CoinDropRange), allowZero: true);
        }
    }

    [Fact]
    public void QuestData_ShouldReferenceKnownLocationsAndPrerequisites()
    {
        var world = ReadWorld();
        var quests = ReadQuests();

        var locationIds = world.Locations.Select(l => l.Id).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var townNames = world.Towns.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var questIds = quests.Select(q => q.Id).ToHashSet(StringComparer.OrdinalIgnoreCase);

        quests.Count.ShouldBeGreaterThan(1);

        foreach (var quest in quests)
        {
            quest.Id.ShouldNotBeNullOrWhiteSpace();
            quest.Name.ShouldNotBeNullOrWhiteSpace();
            quest.Description.ShouldNotBeNullOrWhiteSpace();

            if (!string.IsNullOrWhiteSpace(quest.TownName))
            {
                townNames.ShouldContain(quest.TownName);
            }

            if (!string.IsNullOrWhiteSpace(quest.AcceptLocationId))
            {
                locationIds.ShouldContain(quest.AcceptLocationId);
            }

            if (!string.IsNullOrWhiteSpace(quest.CompletionLocationId))
            {
                locationIds.ShouldContain(quest.CompletionLocationId);
            }

            foreach (var prerequisite in quest.PrerequisiteQuestIds)
            {
                questIds.ShouldContain(prerequisite);
                prerequisite.ShouldNotBe(quest.Id);
            }

            quest.RewardItems.Count.ShouldBeGreaterThan(0);
            quest.RewardItems.ShouldAllBe(r => !string.IsNullOrWhiteSpace(r.ItemId) && r.Quantity > 0);
            quest.ExperienceReward.ShouldBeGreaterThanOrEqualTo(0);
            quest.CoinReward.ShouldBeGreaterThanOrEqualTo(0);
        }
    }

    [Fact]
    public void VendorPricing_ShouldAlignWithTowns()
    {
        var world = ReadWorld();
        var state = ReadVendorPricing();

        state.ValidUntil.ShouldBeGreaterThan(DateTimeOffset.MinValue);
        state.Modifiers.Count.ShouldBeGreaterThan(0);

        var towns = world.Towns.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var modifier in state.Modifiers)
        {
            towns.ShouldContain(modifier.TownName);
            modifier.ItemId.ShouldNotBeNullOrWhiteSpace();
            modifier.BuyMultiplier.ShouldBeGreaterThan(0m);
            modifier.SellMultiplier.ShouldBeGreaterThan(0m);
            modifier.ExpiresAt.ShouldBeGreaterThan(DateTimeOffset.MinValue);
        }
    }

    private WorldState ReadWorld()
    {
        var worldPath = Path.Combine(FindSolutionRoot(), "Data", "world.json");
        var content = File.ReadAllText(worldPath);
        var world = JsonSerializer.Deserialize<List<WorldState>>(content, _jsonOptions);
        world.ShouldNotBeNull();
        world!.Count.ShouldBeGreaterThan(0);
        return world[0];
    }

    private List<Quest> ReadQuests()
    {
        var questsPath = Path.Combine(FindSolutionRoot(), "Data", "quests.json");
        var content = File.ReadAllText(questsPath);
        var quests = JsonSerializer.Deserialize<List<Quest>>(content, _jsonOptions);
        quests.ShouldNotBeNull();
        return quests!;
    }

    private VendorPricingState ReadVendorPricing()
    {
        var pricingPath = Path.Combine(FindSolutionRoot(), "Data", "vendor-pricing.json");
        var content = File.ReadAllText(pricingPath);
        var states = JsonSerializer.Deserialize<List<VendorPricingState>>(content, _jsonOptions);
        states.ShouldNotBeNull();
        states!.Count.ShouldBeGreaterThan(0);
        return states[0];
    }

    private static string FindSolutionRoot()
    {
        var directory = Directory.GetCurrentDirectory();
        while (!string.IsNullOrEmpty(directory))
        {
            if (File.Exists(Path.Combine(directory, "TextAdventure.sln")))
            {
                return directory;
            }

            directory = Directory.GetParent(directory)?.FullName;
        }

        throw new InvalidOperationException("Unable to locate solution root from current directory");
    }

    private class VendorPricingState
    {
        public DateTimeOffset ValidUntil { get; set; }

        public List<VendorPriceModifier> Modifiers { get; set; } = [];
    }

    private static void AssertRange(MonsterStatRange? range, string property, bool allowZero = false)
    {
        range.ShouldNotBeNull();
        range!.Min.ShouldBeGreaterThanOrEqualTo(allowZero ? 0 : 1, property);
        range.Max.ShouldBeGreaterThanOrEqualTo(range.Min, property);
    }
}
