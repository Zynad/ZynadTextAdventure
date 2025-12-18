using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TextAdventure.Infrastructure.Database;

namespace TextAdventureTests.Database;

public class JsonDatabaseSerializationTests : IDisposable
{
    private readonly string _tempDirectory;
    private readonly JsonDatabase _database;

    public JsonDatabaseSerializationTests()
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
    }

    [Fact]
    public async Task ReadAsync_HandlesCamelCaseJsonFromSystemText()
    {
        var databaseModel = DatabaseModel.CreateDefault();

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        serializerOptions.Converters.Add(new JsonStringEnumConverter());

        await using (var stream = File.Create(Path.Combine(_tempDirectory, "database.json")))
        {
            await JsonSerializer.SerializeAsync(stream, databaseModel, serializerOptions);
        }

        var result = await _database.ReadAsync();

        result.Users.ShouldBeEmpty();
        result.Monsters.ShouldNotBeEmpty();
        result.Monsters.ShouldContain(m => m.Name == databaseModel.Monsters.First().Name);
    }

    [Fact]
    public async Task ReadAsync_SeedsDefaultEquipment_WhenFileMissing()
    {
        var defaults = DatabaseModel.CreateDefault();
        var result = await _database.ReadAsync();

        result.Items.Count.ShouldBe(defaults.Items.Count);
        result.Helmets.Count.ShouldBe(defaults.Helmets.Count);
        result.Gloves.Count.ShouldBe(defaults.Gloves.Count);
        result.Chests.Count.ShouldBe(defaults.Chests.Count);
        result.Legs.Count.ShouldBe(defaults.Legs.Count);
        result.Boots.Count.ShouldBe(defaults.Boots.Count);
        result.Swords.Count.ShouldBe(defaults.Swords.Count);
        result.Axes.Count.ShouldBe(defaults.Axes.Count);
        result.Wands.Count.ShouldBe(defaults.Wands.Count);
        result.Staff.Count.ShouldBe(defaults.Staff.Count);
    }

    [Fact]
    public async Task ReadAsync_RecreatesDefaults_WhenJsonMalformed()
    {
        await File.WriteAllTextAsync(Path.Combine(_tempDirectory, "database.json"), "{ invalid json }");

        var result = await _database.ReadAsync();

        result.Monsters.ShouldNotBeEmpty();
        result.Helmets.ShouldContain(h => h.Name == "Leather Hood");
        result.Swords.ShouldContain(s => s.Name == "Steel Longsword");
        result.Wands.ShouldContain(w => w.Name == "Apprentice Wand");
        result.Staff.ShouldContain(s => s.Name == "Elderwood Staff");
        result.Axes.ShouldContain(a => a.Name == "Woodcutter's Axe");
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
