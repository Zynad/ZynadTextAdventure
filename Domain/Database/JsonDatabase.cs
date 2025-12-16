using System.Text.Json;
using Domain.Entities.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.Database;

public class JsonDatabaseOptions
{
    public string DatabasePath { get; set; } = Path.Combine("Data", "database.json");
}

public class JsonDatabase : IGameDatabase
{
    private readonly string _databasePath;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly ILogger<JsonDatabase> _logger;
    private readonly IHostEnvironment _environment;

    public JsonDatabase(IOptions<JsonDatabaseOptions> options, ILogger<JsonDatabase> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
        _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var basePath = string.IsNullOrWhiteSpace(_environment.ContentRootPath)
            ? AppContext.BaseDirectory
            : _environment.ContentRootPath;
        var configuredPath = options.Value.DatabasePath;
        _databasePath = string.IsNullOrWhiteSpace(configuredPath)
            ? Path.Combine(basePath, "Data", "database.json")
            : Path.IsPathRooted(configuredPath)
                ? configuredPath
                : Path.GetFullPath(configuredPath, basePath);

        var directory = Path.GetDirectoryName(_databasePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public async Task<DatabaseModel> ReadAsync(CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            if (!File.Exists(_databasePath))
            {
                var database = DatabaseModel.CreateDefault();
                await WriteInternalAsync(database, cancellationToken);
                return database;
            }

            await using var stream = File.OpenRead(_databasePath);
            var databaseModel = await JsonSerializer.DeserializeAsync<DatabaseModel>(stream, _serializerOptions, cancellationToken)
                                ?? DatabaseModel.CreateDefault();

            if (databaseModel.Monsters.Count == 0)
            {
                databaseModel.Monsters.AddRange(DatabaseModel.CreateDefaultMonsters());
            }

            return databaseModel;
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Failed to parse database file at {Path}. Recreating with defaults.", _databasePath);
            var database = DatabaseModel.CreateDefault();
            await WriteInternalAsync(database, cancellationToken);
            return database;
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task WriteAsync(DatabaseModel databaseModel, CancellationToken cancellationToken = default)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            await WriteInternalAsync(databaseModel, cancellationToken);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task WriteInternalAsync(DatabaseModel databaseModel, CancellationToken cancellationToken)
    {
        var directory = Path.GetDirectoryName(_databasePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var tempFile = Path.GetTempFileName();
        await using (var stream = File.Create(tempFile))
        {
            await JsonSerializer.SerializeAsync(stream, databaseModel, _serializerOptions, cancellationToken);
        }

        try
        {
            if (File.Exists(_databasePath))
            {
                File.Replace(tempFile, _databasePath, null);
            }
            else
            {
                File.Move(tempFile, _databasePath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist JSON database to {Path}", _databasePath);
            throw;
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
