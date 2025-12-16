using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.Entities.Storage;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Adventure;

public record EncounterResolution(Encounter Encounter, IReadOnlyCollection<InventoryItem> Loot);

public class EncounterGenerator
{
    private readonly IRandomService _randomService;
    private readonly IWorldRepository _worldRepository;
    private readonly ILogger<EncounterGenerator> _logger;

    private static readonly Dictionary<string, double> ThreatChances = new(StringComparer.OrdinalIgnoreCase)
    {
        { "safe", 0.05 },
        { "calm", 0.1 },
        { "low", 0.18 },
        { "moderate", 0.28 },
        { "high", 0.45 },
        { "extreme", 0.6 }
    };

    public EncounterGenerator(IRandomService randomService, IWorldRepository worldRepository, ILogger<EncounterGenerator> logger)
    {
        _randomService = randomService;
        _worldRepository = worldRepository;
        _logger = logger;
    }

    public async Task<EncounterResolution?> GenerateForTravelAsync(
        Character character,
        WorldLocationNode origin,
        WorldLocationNode destination,
        CancellationToken cancellationToken = default)
    {
        var chance = CalculateEncounterChance(destination.ThreatLevel);
        var roll = _randomService.NextDouble();
        if (roll > chance)
        {
            _logger.LogDebug("No encounter rolled for {CharacterId} between {Origin} and {Destination} (roll: {Roll})", character.Id, origin.Name, destination.Name, roll);
            return null;
        }

        var monsters = (await _worldRepository.GetMonstersAsync(cancellationToken)).ToList();
        var dropTables = await _worldRepository.GetDropTablesAsync(cancellationToken);
        var dropPool = GetDropTable(dropTables, destination.Biome);
        if (monsters.Count == 0 || _randomService.NextDouble() < 0.25)
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

            _logger.LogInformation(
                "Character {CharacterId} experienced discovery at {Location} with {DropCount} drops",
                character.Id,
                destination.Name,
                discoveryDrops.Count);

            return new EncounterResolution(discovery, discoveryDrops);
        }

        var monster = monsters[_randomService.NextInt(0, monsters.Count)];
        var victory = ResolveCombat(character.Level, monster.Level);
        var loot = victory ? RollDrops(dropPool, 3) : new List<InventoryItem>();

        var battle = new Encounter
        {
            CharacterId = character.Id,
            MonsterId = monster.Id,
            EncounterType = "Battle",
            Location = destination.Name,
            Outcome = victory ? "Victory" : "Defeat",
            Drops = loot
        };

        _logger.LogInformation(
            "Character {CharacterId} encountered monster {MonsterId} at {Location} with outcome {Outcome}",
            character.Id,
            monster.Id,
            destination.Name,
            battle.Outcome);

        return new EncounterResolution(battle, loot);
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

    private bool ResolveCombat(int characterLevel, int monsterLevel)
    {
        var advantage = characterLevel - monsterLevel;
        var baseChance = 0.5 + (advantage * 0.08);
        baseChance = Math.Clamp(baseChance, 0.25, 0.9);

        var roll = _randomService.NextDouble();
        return roll <= baseChance;
    }

    private List<InventoryItem> RollDrops(List<string> pool, int maxStacks)
    {
        if (pool.Count == 0)
        {
            return new List<InventoryItem>();
        }
        var dropCount = _randomService.NextInt(1, Math.Max(2, maxStacks + 1));
        var drops = new List<InventoryItem>();

        for (var i = 0; i < dropCount; i++)
        {
            var dropId = pool[_randomService.NextInt(0, pool.Count)];
            var quantity = _randomService.NextInt(1, 4);
            AddOrIncrementDrop(drops, dropId, quantity);
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
            return new List<string> { "coin_pouch", "mysterious_trinket", "tattered_map" };
        }

        var match = dropTables.FirstOrDefault(dt => dt.Biome.Equals(biome, StringComparison.OrdinalIgnoreCase));
        if (match?.Drops.Count > 0)
        {
            return match.Drops;
        }

        var fallback = dropTables.FirstOrDefault(dt => dt.Biome.Equals("Unknown", StringComparison.OrdinalIgnoreCase));
        return fallback?.Drops.Count > 0
            ? fallback.Drops
            : new List<string> { "coin_pouch", "mysterious_trinket", "tattered_map" };
    }
}
