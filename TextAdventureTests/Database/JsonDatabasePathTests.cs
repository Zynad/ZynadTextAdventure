using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TextAdventure.Infrastructure.Database;

namespace TextAdventureTests.Database;

public class JsonDatabasePathTests : IDisposable
{
    private readonly string _contentRoot;
    private readonly JsonDatabase _database;

    public JsonDatabasePathTests()
    {
        _contentRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_contentRoot);

        var options = Options.Create(new JsonDatabaseOptions
        {
            DatabasePath = Path.Combine("nested", "db.json")
        });

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_contentRoot);

        _database = new JsonDatabase(options, NullLogger<JsonDatabase>.Instance, environment);
    }

    [Fact]
    public async Task ReadAsync_CreatesDatabaseWithinContentRootForRelativePath()
    {
        var database = await _database.ReadAsync();

        database.ShouldNotBeNull();
        File.Exists(Path.Combine(_contentRoot, "nested", "db.json")).ShouldBeTrue();
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_contentRoot))
            {
                Directory.Delete(_contentRoot, true);
            }
        }
        catch
        {
            // best effort cleanup
        }
    }
}
