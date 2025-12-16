using ApplicationServices.Contracts.Requests;
using ApplicationServices.Services;
using Domain.Database;
using AutoFixture;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;

namespace TextAdventureTests.Services;

public class GameDataServiceTests : IDisposable
{
    private readonly Fixture _fixture = new();
    private readonly string _tempDirectory;
    private readonly JsonDatabase _database;
    private readonly GameDataService _service;

    public GameDataServiceTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDirectory);

        var options = Options.Create(new JsonDatabaseOptions
        {
            DatabasePath = Path.Combine(_tempDirectory, "database.json")
        });

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_tempDirectory);

        _database = new JsonDatabase(options, NullLogger<JsonDatabase>.Instance, environment);
        _service = new GameDataService(_database);
    }

    [Fact]
    public async Task RegisterAsync_PersistsUserWithToken()
    {
        var response = await _service.RegisterAsync(new RegisterRequest
        {
            Username = "player1",
            Password = "secret"
        });

        response.ShouldNotBeNull();
        response!.Token.ShouldNotBeNullOrWhiteSpace();

        var data = await _database.ReadAsync();
        data.Users.Single().Username.ShouldBe("player1");
    }

    [Fact]
    public async Task RegisterAsync_ReturnsNullForDuplicateUser()
    {
        await _service.RegisterAsync(new RegisterRequest { Username = "player1", Password = "secret" });
        var secondAttempt = await _service.RegisterAsync(new RegisterRequest { Username = "player1", Password = "secret" });

        secondAttempt.ShouldBeNull();
    }

    [Fact]
    public async Task SaveProgressAsync_CreatesSaveSlotWithLocation()
    {
        var auth = await _service.RegisterAsync(new RegisterRequest { Username = "player1", Password = "secret" });
        auth.ShouldNotBeNull();

        var locationName = _fixture.Create<string>();
        var locationBiome = _fixture.Create<string>();

        var saveResult = await _service.SaveProgressAsync(new SaveProgressRequest
        {
            Token = auth!.Token,
            Level = 3,
            Experience = 150,
            AdventureState = "Crossroads",
            SaveSlotName = "Slot A",
            LocationName = locationName,
            LocationBiome = locationBiome,
            LocationThreatLevel = "Medium"
        });

        saveResult.ShouldBeTrue();

        var progress = await _service.GetProgressAsync(auth.Token);
        progress.ShouldNotBeNull();
        progress!.SaveSlots.ShouldContain(slot => slot.Name == "Slot A" && slot.Location.Biome == locationBiome);
    }

    [Fact]
    public async Task GetMonstersAsync_ReturnsDefaultCatalog()
    {
        var monsters = await _service.GetMonstersAsync();

        monsters.Count.ShouldBeGreaterThanOrEqualTo(5);
        monsters.ShouldContain(m => m.Name == "Bandit Captain");
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }
        catch
        {
            // best effort cleanup
        }
    }
}
