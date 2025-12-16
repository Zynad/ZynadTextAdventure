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

    public async Task SaveWorldAsync(
        IEnumerable<Town> towns,
        IEnumerable<Monster> monsters,
        IEnumerable<CharacterPreset> characterPresets,
        CancellationToken cancellationToken = default)
    {
        var world = new WorldState
        {
            Towns = towns.ToList(),
            Monsters = monsters.ToList(),
            CharacterPresets = characterPresets.ToList()
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

        return world;
    }

    private static WorldState CreateDefaultWorldState()
    {
        return new WorldState
        {
            Towns = new List<Town>(),
            Monsters = new List<Monster>(),
            CharacterPresets = CreateDefaultCharacterPresets()
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
