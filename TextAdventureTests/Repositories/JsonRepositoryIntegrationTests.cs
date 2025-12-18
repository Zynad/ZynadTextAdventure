using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using System.Text.Json;
using System.Text.Json.Serialization;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Repositories;

namespace TextAdventureTests.Repositories;

public class JsonRepositoryIntegrationTests : IDisposable
{
    private readonly string _dataDirectory;
    private readonly string _tempDataDirectory;
    private readonly string _originalAccountsContent;
    private readonly string _originalQuestsContent;
    private readonly JsonUserRepository _userRepository;
    private readonly JsonCharacterRepository _characterRepository;
    private readonly JsonQuestRepository _questRepository;
    private readonly JsonWorldRepository _worldRepository;

    public JsonRepositoryIntegrationTests()
    {
        _dataDirectory = Path.Combine(FindSolutionRoot(), "Data");
        _originalAccountsContent = File.ReadAllText(Path.Combine(_dataDirectory, "accounts.json"));
        _originalQuestsContent = File.ReadAllText(Path.Combine(_dataDirectory, "quests.json"));

        _tempDataDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDataDirectory);
        CopyDataDirectory(_dataDirectory, _tempDataDirectory);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Test.json", optional: false)
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["DataStore:DataDirectory"] = _tempDataDirectory
            })
            .Build();

        var dataStoreOptions = configuration.GetSection("DataStore").Get<DataStoreOptions>() ?? new DataStoreOptions();
        var optionsWrapper = Options.Create(dataStoreOptions);

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_tempDataDirectory);

        var concurrencyProvider = new FileConcurrencyProvider();
        _userRepository = new JsonUserRepository(optionsWrapper, environment, NullLogger<JsonUserRepository>.Instance, concurrencyProvider);
        _characterRepository = new JsonCharacterRepository(optionsWrapper, environment, NullLogger<JsonCharacterRepository>.Instance, concurrencyProvider);
        _questRepository = new JsonQuestRepository(optionsWrapper, environment, NullLogger<JsonQuestRepository>.Instance, concurrencyProvider);
        _worldRepository = new JsonWorldRepository(optionsWrapper, environment, NullLogger<JsonWorldRepository>.Instance, concurrencyProvider);
    }

    [Fact]
    public async Task RepositoriesPersistChangesIntoTemporaryStore()
    {
        var account = new Account
        {
            Username = "copy-user",
            Email = "copy@example.com",
            PasswordHash = "hash",
            PasswordSalt = "salt"
        };

        await _userRepository.AddAsync(account);

        var character = new Character
        {
            AccountId = account.Id,
            Name = "Temp Character",
            PresetId = "warrior",
            ClassName = "Warrior",
            Location = WorldLocation.Default(),
            Inventory = [new() { ItemId = "loaf_of_bread", Quantity = 1 }]
        };

        await _characterRepository.AddAsync(character);

        var newQuest = new Quest
        {
            Id = "scout_outskirts",
            Name = "Scout the outskirts",
            AcceptLocationId = "emberbrook_square"
        };

        await _questRepository.AddAsync(newQuest);

        File.ReadAllText(Path.Combine(_tempDataDirectory, "accounts.json")).ShouldContain(account.Email);
        File.ReadAllText(Path.Combine(_tempDataDirectory, "characters.json")).ShouldContain(character.Name);
        File.ReadAllText(Path.Combine(_tempDataDirectory, "quests.json")).ShouldContain(newQuest.Id);

        File.ReadAllText(Path.Combine(_dataDirectory, "accounts.json")).ShouldBe(_originalAccountsContent);
        File.ReadAllText(Path.Combine(_dataDirectory, "quests.json")).ShouldBe(_originalQuestsContent);
    }

    [Fact]
    public async Task WorldRepository_LoadsCopiedWorldData()
    {
        var towns = await _worldRepository.GetTownsAsync();
        var locations = await _worldRepository.GetLocationsAsync();
        locations.ShouldContain(l => l.Name == "Traveler's Road");

        towns.ShouldContain(t => t.Name == "Sunspire");
        towns.ShouldContain(t => t.Name == "Glimmerdeep");

        locations.ShouldContain(l => l.Id == "sunspire_oasis" && l.ThreatLevel == "Perilous");

        var dropTables = await _worldRepository.GetDropTablesAsync();
        dropTables.Count.ShouldBe(12);
        dropTables.ShouldContain(t => t.Biome == "Desert" && t.Drops.Contains("dune_blade"));
        dropTables.ShouldContain(t => t.Biome == "Underground" && t.Drops.Contains("glow_mushroom"));

        var presets = await _worldRepository.GetCharacterPresetsAsync();
        presets.ShouldContain(p => p.Id == "warrior");

        var monsters = await _worldRepository.GetMonstersAsync();
        monsters.ShouldContain(m => m.Id == "road_bandit");
        monsters.ShouldContain(m => m.Id == "sand_wraith");
    }

    [Fact]
    public async Task UserRepository_ReadsCamelCaseSerializedAccounts()
    {
        var camelCaseAccount = new Account
        {
            Username = "camel_user",
            Email = "camel@example.com",
            PasswordHash = "hash",
            PasswordSalt = "salt"
        };

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        serializerOptions.Converters.Add(new JsonStringEnumConverter());

        await using (var stream = File.Create(Path.Combine(_tempDataDirectory, "accounts.json")))
        {
            await JsonSerializer.SerializeAsync(stream, new List<Account> { camelCaseAccount }, serializerOptions);
        }

        var account = await _userRepository.GetByUsernameAsync(camelCaseAccount.Username);

        account.ShouldNotBeNull();
        account!.Email.ShouldBe(camelCaseAccount.Email);
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_tempDataDirectory))
            {
                Directory.Delete(_tempDataDirectory, true);
            }
        }
        catch
        {
            // best effort cleanup
        }
    }

    private static void CopyDataDirectory(string source, string destination)
    {
        foreach (var file in Directory.GetFiles(source))
        {
            var fileName = Path.GetFileName(file);
            File.Copy(file, Path.Combine(destination, fileName));
        }
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
}
