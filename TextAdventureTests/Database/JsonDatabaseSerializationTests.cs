using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;

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

        result.Users.ShouldNotBeEmpty();
        result.Monsters.ShouldNotBeEmpty();
        result.Monsters.ShouldContain(m => m.Name == databaseModel.Monsters.First().Name);
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
