using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Entities.Storage;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

internal class WorldState
{
    public List<Town> Towns { get; set; } = new();
    public List<Monster> Monsters { get; set; } = new();
    public List<CharacterPreset> CharacterPresets { get; set; } = new();
    public List<WorldLocationNode> Locations { get; set; } = new();
}

public class JsonWorldRepository : IWorldRepository
{
    private readonly JsonFileStore<WorldState> _store;

    public JsonWorldRepository(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger<JsonWorldRepository> logger, FileConcurrencyProvider concurrencyProvider)
    {
        _store = new JsonFileStore<WorldState>(options, environment, logger, concurrencyProvider, options.Value.WorldFileName);
    }

    public async Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Monsters;
    }

    public async Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(
        CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.CharacterPresets;
    }

    public async Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Towns;
    }

    public async Task<IReadOnlyCollection<WorldLocationNode>> GetLocationsAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Locations;
    }

    public async Task SaveWorldAsync(
        IEnumerable<Town> towns,
        IEnumerable<Monster> monsters,
        IEnumerable<CharacterPreset> characterPresets,
        IEnumerable<WorldLocationNode> locations,
        CancellationToken cancellationToken = default)
    {
        var world = new WorldState
        {
            Towns = towns.ToList(),
            Monsters = monsters.ToList(),
            CharacterPresets = characterPresets.ToList(),
            Locations = locations.ToList()
        };

        await _store.WriteAsync(new[] { world }, cancellationToken);
    }

    private async Task<WorldState> ReadWorldAsync(CancellationToken cancellationToken)
    {
        var data = await _store.ReadAsync(() => new List<WorldState> { CreateDefaultWorldState() }, cancellationToken);
        var world = data.FirstOrDefault() ?? CreateDefaultWorldState();

        if (world.CharacterPresets.Count == 0)
        {
            world.CharacterPresets.AddRange(CreateDefaultCharacterPresets());
        }

        if (world.Monsters.Count == 0)
        {
            world.Monsters.AddRange(CreateDefaultMonsters());
        }

        if (world.Locations.Count == 0)
        {
            world.Locations.AddRange(CreateDefaultLocations());
        }

        return world;
    }

    private static WorldState CreateDefaultWorldState()
    {
        return new WorldState
        {
            Towns = new List<Town>(),
            Monsters = CreateDefaultMonsters(),
            CharacterPresets = CreateDefaultCharacterPresets(),
            Locations = CreateDefaultLocations()
        };
    }

    private static List<Monster> CreateDefaultMonsters()
    {
        return new List<Monster>
        {
            new()
            {
                Id = "road_bandit",
                Name = "Roadside Bandit",
                Level = 1,
                HitPoints = 10,
                Attack = 3
            },
            new()
            {
                Id = "wild_boar",
                Name = "Wild Boar",
                Level = 2,
                HitPoints = 14,
                Attack = 4
            },
            new()
            {
                Id = "emberbrook_scout",
                Name = "Wayward Scout",
                Level = 3,
                HitPoints = 16,
                Attack = 5
            }
        };
    }

    private static List<WorldLocationNode> CreateDefaultLocations()
    {
        return new List<WorldLocationNode>
        {
            new()
            {
                Id = "travelers_road",
                Name = "Traveler's Road",
                Description = "A well-worn path where many adventurers begin their journey.",
                Biome = "Grassland",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "forked_path" }
            },
            new()
            {
                Id = "forked_path",
                Name = "Forked Path",
                Description = "A crossroads lined with signposts pointing toward nearby settlements.",
                Biome = "Grassland",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "travelers_road", "emberbrook_gate" }
            },
            new()
            {
                Id = "emberbrook_gate",
                Name = "Emberbrook Gate",
                Description = "The sturdy wooden gate guarding the village of Emberbrook.",
                Biome = "Village",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "forked_path", "emberbrook_square" },
                TownName = "Emberbrook"
            },
            new()
            {
                Id = "emberbrook_square",
                Name = "Emberbrook Square",
                Description = "A bustling square where villagers trade stories and goods.",
                Biome = "Village",
                ThreatLevel = "Safe",
                AdjacentLocationIds = new List<string> { "emberbrook_gate" },
                TownName = "Emberbrook"
            }
        };
    }

    private static List<CharacterPreset> CreateDefaultCharacterPresets()
    {
        return new List<CharacterPreset>
        {
            new()
            {
                Id = "warrior",
                Name = "Warrior",
                Description = "A seasoned fighter with dependable gear.",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "rusty_sword", Quantity = 1 },
                    new() { ItemId = "worn_shield", Quantity = 1 },
                    new() { ItemId = "loaf_of_bread", Quantity = 3 }
                }
            },
            new()
            {
                Id = "ranger",
                Name = "Ranger",
                Description = "A nimble hunter who travels light and strikes from range.",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "shortbow", Quantity = 1 },
                    new() { ItemId = "quiver_of_arrows", Quantity = 20 },
                    new() { ItemId = "traveler_cloak", Quantity = 1 }
                }
            },
            new()
            {
                Id = "mystic",
                Name = "Mystic",
                Description = "A student of the arcane starting their journey with basic focus tools.",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "oak_staff", Quantity = 1 },
                    new() { ItemId = "apprentice_robes", Quantity = 1 },
                    new() { ItemId = "healing_herbs", Quantity = 2 }
                }
            }
        };
    }
}
