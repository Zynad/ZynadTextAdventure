using System.Linq;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Repositories;

namespace TextAdventureTests.Repositories;

public class RepositoryMappingTests : IDisposable
{
    private readonly string _dataDirectory;
    private readonly JsonCharacterRepository _characterRepository;
    private readonly JsonWorldRepository _worldRepository;
    private readonly FileConcurrencyProvider _concurrencyProvider;

    public RepositoryMappingTests()
    {
        _dataDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_dataDirectory);

        var options = Options.Create(new DataStoreOptions
        {
            DataDirectory = _dataDirectory
        });

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_dataDirectory);

        _concurrencyProvider = new FileConcurrencyProvider();
        _characterRepository = new JsonCharacterRepository(options, environment, NullLogger<JsonCharacterRepository>.Instance, _concurrencyProvider);
        _worldRepository = new JsonWorldRepository(options, environment, NullLogger<JsonWorldRepository>.Instance, _concurrencyProvider);
    }

    [Fact]
    public async Task CharacterRepository_MapsStorageModelToDomain()
    {
        var location = new WorldLocation { Name = "Mapped Plains", Biome = "Grassland", ThreatLevel = "Low" };
        var character = new Character
        {
            Name = "Mapper",
            AccountId = Guid.NewGuid(),
            ClassName = "Rogue",
            Level = 3,
            Coins = 42,
            Location = location
        };

        await _characterRepository.AddAsync(character);

        var stored = await _characterRepository.GetByIdAsync(character.Id);

        stored.ShouldNotBeNull();
        stored!.Location.Name.ShouldBe(location.Name);
        stored.Location.Biome.ShouldBe(location.Biome);
        stored.Location.ThreatLevel.ShouldBe(location.ThreatLevel);
        stored.Coins.ShouldBe(character.Coins);
        stored.Level.ShouldBe(character.Level);
    }

    [Fact]
    public async Task WorldRepository_RoundTripsDomainWorldState()
    {
        var location = new WorldLocationNode
        {
            Id = "mapper_trail",
            Name = "Mapper's Trail",
            Description = "A winding path used for mapping exercises.",
            Biome = "Forest",
            ThreatLevel = "Moderate",
            AdjacentLocationIds = new List<string> { "cartographer_camp" }
        };

        var preset = new CharacterPreset
        {
            Id = "mapper",
            Name = "Mapper",
            Description = "Starts with a map and compass.",
            StartingLocation = new WorldLocation { Name = "Cartographer Camp", Biome = "Forest", ThreatLevel = "Low" },
            StartingInventory = new List<InventoryItem> { new() { ItemId = "map", Quantity = 1 } }
        };

        var monsters = new List<Monster>
        {
            new()
            {
                Id = "trail_wolf",
                Name = "Trail Wolf",
                Biome = "Forest",
                LevelRange = new MonsterStatRange { Min = 2, Max = 4 },
                HitPointRange = new MonsterStatRange { Min = 10, Max = 14 },
                AttackRange = new MonsterStatRange { Min = 3, Max = 5 },
                DefenseRange = new MonsterStatRange { Min = 1, Max = 2 },
                CoinDropRange = new MonsterStatRange { Min = 1, Max = 3 },
                PreferredThreatLevels = new List<string> { "Moderate" }
            }
        };

        var dropTables = new List<DropTable>
        {
            new() { Biome = "Forest", Drops = new List<string> { "map", "compass" } }
        };

        await _worldRepository.SaveWorldAsync(
            new[] { new Town { Name = "Cartographer Camp" } },
            monsters,
            new[] { preset },
            new[] { location },
            dropTables);

        var reloadedLocation = (await _worldRepository.GetLocationsAsync()).Single(l => l.Id == location.Id);
        reloadedLocation.Name.ShouldBe(location.Name);
        reloadedLocation.Biome.ShouldBe(location.Biome);
        reloadedLocation.AdjacentLocationIds.ShouldContain("cartographer_camp");

        var reloadedPreset = (await _worldRepository.GetCharacterPresetsAsync()).Single(p => p.Id == preset.Id);
        reloadedPreset.StartingLocation.Name.ShouldBe(preset.StartingLocation.Name);
        reloadedPreset.StartingInventory.ShouldContain(i => i.ItemId == "map");
    }

    public void Dispose()
    {
        if (Directory.Exists(_dataDirectory))
        {
            Directory.Delete(_dataDirectory, true);
        }
    }
}
