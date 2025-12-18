using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Adventure;

public record EncounterResolution(Encounter Encounter, IReadOnlyCollection<InventoryItem> Loot, int Experience, int Coins);
public record MonsterSpawn(Monster Template, int Level, int HitPoints, int Attack, int Defense, int Coins);

public class EncounterGenerator(
    IRandomService randomService,
    IWorldRepository worldRepository,
    ILogger<EncounterGenerator> logger)
{
    private static readonly Dictionary<string, double> ThreatChances = new(StringComparer.OrdinalIgnoreCase)
    {
        { "safe", 0.05 },
        { "calm", 0.1 },
        { "low", 0.18 },
        { "moderate", 0.28 },
        { "high", 0.45 },
        { "extreme", 0.6 }
    };

    private static readonly Dictionary<string, (int Min, int Max)> ThreatLevelRanges = new(StringComparer.OrdinalIgnoreCase)
    {
        { "safe", (1, 1) },
        { "calm", (1, 2) },
        { "low", (1, 4) },
        { "moderate", (2, 7) },
        { "high", (4, 10) },
        { "extreme", (6, 12) }
    };

    public async Task<EncounterResolution?> GenerateForTravelAsync(
        Character character,
        WorldLocationNode origin,
        WorldLocationNode destination,
        CancellationToken cancellationToken = default)
    {
        var chance = CalculateEncounterChance(destination.ThreatLevel);
        var roll = randomService.NextDouble();
        if (roll > chance)
        {
            logger.LogDebug("No encounter rolled for {CharacterId} between {Origin} and {Destination} (roll: {Roll})", character.Id, origin.Name, destination.Name, roll);
            return null;
        }

        var monsters = (await worldRepository.GetMonstersAsync(cancellationToken)).ToList();
        var dropTables = await worldRepository.GetDropTablesAsync(cancellationToken);
        var dropPool = GetDropTable(dropTables, destination.Biome);
        if (monsters.Count == 0)
        {
            return BuildDiscovery(character, destination, dropPool);
        }

        if (randomService.NextDouble() < 0.05)
        {
            return BuildDiscovery(character, destination, dropPool);
        }

        var monsterCandidates = FilterMonsters(monsters, destination.Biome, destination.ThreatLevel).ToList();
        var spawn = RollMonsterSpawn(monsterCandidates, destination.ThreatLevel);
        var victory = ResolveCombat(character.Level, spawn);
        var loot = victory ? RollDrops(dropPool, 3, spawn) : [];
        var experience = CalculateExperienceReward(spawn, victory);
        var coins = loot.FirstOrDefault(l => l.ItemId.Equals("coins", StringComparison.OrdinalIgnoreCase))?.Quantity ?? 0;

        var battle = new Encounter
        {
            CharacterId = character.Id,
            MonsterId = spawn.Template.Id,
            EncounterType = "Battle",
            Location = destination.Name,
            Outcome = victory ? "Victory" : "Defeat",
            Drops = loot
        };

        logger.LogInformation(
            "Character {CharacterId} encountered monster {MonsterId} at {Location} with outcome {Outcome}",
            character.Id,
            spawn.Template.Id,
            destination.Name,
            battle.Outcome);

        return new EncounterResolution(battle, loot, experience, coins);
    }

    private double CalculateEncounterChance(string threatLevel)
    {
        if (string.IsNullOrWhiteSpace(threatLevel))
        {
            return ThreatChances["low"];
        }

        if (ThreatChances.TryGetValue(threatLevel, out var chance))
        {
            return chance;
        }

        return ThreatChances.GetValueOrDefault(threatLevel.ToLowerInvariant(), 0.18);
    }

    private bool ResolveCombat(int characterLevel, MonsterSpawn spawn)
    {
        var advantage = characterLevel - spawn.Level;
        var defensePenalty = spawn.Defense * 0.02;
        var attackPenalty = Math.Max(0, (spawn.Attack - 4) * 0.01);
        var baseChance = 0.55 + (advantage * 0.07) - defensePenalty - attackPenalty;
        baseChance = Math.Clamp(baseChance, 0.2, 0.9);

        var roll = randomService.NextDouble();
        return roll <= baseChance;
    }

    private List<InventoryItem> RollDrops(List<string> pool, int maxStacks, MonsterSpawn? spawn = null)
    {
        if (pool.Count == 0)
        {
            return [];
        }
        var dropCount = randomService.NextInt(1, Math.Max(2, maxStacks + 1));
        var drops = new List<InventoryItem>();

        for (var i = 0; i < dropCount; i++)
        {
            var dropId = pool[randomService.NextInt(0, pool.Count)];
            var quantity = randomService.NextInt(1, 4);
            AddOrIncrementDrop(drops, dropId, quantity);
        }

        if (spawn is not null && spawn.Coins > 0 && randomService.NextDouble() <= 0.65)
        {
            AddOrIncrementDrop(drops, "coins", spawn.Coins);
        }

        return drops;
    }

    private static void AddOrIncrementDrop(ICollection<InventoryItem> drops, string dropId, int quantity)
    {
        var existing = drops.FirstOrDefault(d => d.ItemId.Equals(dropId, StringComparison.OrdinalIgnoreCase));
        if (existing is not null)
        {
            existing.Quantity += quantity;
            return;
        }

        drops.Add(new InventoryItem { ItemId = dropId, Quantity = quantity });
    }

    private List<string> GetDropTable(IReadOnlyCollection<DropTable> dropTables, string biome)
    {
        if (dropTables.Count == 0)
        {
            return ["coin_pouch", "mysterious_trinket", "tattered_map"];
        }

        var match = dropTables.FirstOrDefault(dt => dt.Biome.Equals(biome, StringComparison.OrdinalIgnoreCase));
        if (match?.Drops.Count > 0)
        {
            return match.Drops;
        }

        var fallback = dropTables.FirstOrDefault(dt => dt.Biome.Equals("Unknown", StringComparison.OrdinalIgnoreCase));
        return fallback?.Drops.Count > 0
            ? fallback.Drops
            : ["coin_pouch", "mysterious_trinket", "tattered_map"];
    }

    private EncounterResolution BuildDiscovery(Character character, WorldLocationNode destination, List<string> dropPool)
    {
        var discoveryDrops = RollDrops(dropPool, 2);
        var discovery = new Encounter
        {
            CharacterId = character.Id,
            EncounterType = "Discovery",
            Location = destination.Name,
            Outcome = discoveryDrops.Count > 0 ? "Found supplies" : "Uneventful",
            Drops = discoveryDrops
        };

        logger.LogInformation(
            "Character {CharacterId} experienced discovery at {Location} with {DropCount} drops",
            character.Id,
            destination.Name,
            discoveryDrops.Count);

        return new EncounterResolution(discovery, discoveryDrops, 0, 0);
    }

    private static int CalculateExperienceReward(MonsterSpawn spawn, bool victory)
    {
        if (!victory)
        {
            return 0;
        }

        var baseExperience = Math.Max(10, spawn.Level * 8);
        var defenseFactor = Math.Max(0, spawn.Defense - 2) * 2;
        var attackFactor = Math.Max(0, spawn.Attack - 3) * 2;

        return baseExperience + defenseFactor + attackFactor;
    }

    private IEnumerable<Monster> FilterMonsters(IEnumerable<Monster> monsters, string biome, string threatLevel)
    {
        var preferred = monsters
            .Where(m => m.Biome.Equals(biome, StringComparison.OrdinalIgnoreCase)
                        && m.PreferredThreatLevels.Any()
                        && m.PreferredThreatLevels.Contains(threatLevel, StringComparer.OrdinalIgnoreCase));

        var biomeMatches = monsters.Where(m => m.Biome.Equals(biome, StringComparison.OrdinalIgnoreCase));

        return preferred.Any() ? preferred : biomeMatches.Any() ? biomeMatches : monsters;
    }

    private MonsterSpawn RollMonsterSpawn(IEnumerable<Monster> monsters, string threatLevel)
    {
        var monsterList = monsters.ToList();
        if (monsterList.Count == 0)
        {
            return new MonsterSpawn(new Monster { Name = "Unknown", Biome = "Unknown" }, 1, 5, 1, 0, 1);
        }

        var choice = monsterList[randomService.NextInt(0, monsterList.Count)];
        var (minThreatLevel, maxThreatLevel) = ThreatLevelRanges.GetValueOrDefault(threatLevel.ToLowerInvariant(), (1, 6));

        int RollInRange(MonsterStatRange range, int defaultMin)
        {
            var min = Math.Max(range.Min, defaultMin);
            var max = Math.Max(min, range.Max);
            return randomService.NextInt(min, max + 1);
        }

        var level = Math.Clamp(RollInRange(choice.LevelRange, 1), minThreatLevel, maxThreatLevel);
        var hp = RollInRange(choice.HitPointRange, level * 3);
        var attack = RollInRange(choice.AttackRange, Math.Max(1, level - 1));
        var defense = RollInRange(choice.DefenseRange, Math.Max(0, level / 2));
        var coins = (int)Math.Round(RollInRange(choice.CoinDropRange, level) * (0.75 + randomService.NextDouble() * 0.5));

        return new MonsterSpawn(choice, level, hp, attack, defense, Math.Max(coins, 0));
    }
}
