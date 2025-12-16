using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

internal class FileConcurrencyProvider
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> Locks = new(StringComparer.OrdinalIgnoreCase);

    public SemaphoreSlim GetLock(string path)
    {
        return Locks.GetOrAdd(path, _ => new SemaphoreSlim(1, 1));
    }
}

internal class JsonFileStore<T>
{
    private readonly string _path;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly SemaphoreSlim _gate;
    private readonly ILogger _logger;

    public JsonFileStore(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger logger, FileConcurrencyProvider concurrencyProvider, string fileName)
    {
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        var basePath = string.IsNullOrWhiteSpace(environment.ContentRootPath)
            ? AppContext.BaseDirectory
            : environment.ContentRootPath;
        var dataDirectory = options.Value.DataDirectory;
        var directory = Path.IsPathRooted(dataDirectory)
            ? dataDirectory
            : Path.GetFullPath(dataDirectory, basePath);

        Directory.CreateDirectory(directory);
        _path = Path.Combine(directory, fileName);
        _gate = concurrencyProvider.GetLock(_path);
    }

    public async Task<List<T>> ReadAsync(Func<List<T>> defaultFactory, CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            if (!File.Exists(_path))
            {
                var defaults = defaultFactory();
                await WriteInternalAsync(defaults, cancellationToken);
                return defaults;
            }

            await using var stream = File.OpenRead(_path);
            var data = await JsonSerializer.DeserializeAsync<List<T>>(stream, _serializerOptions, cancellationToken);
            return data ?? defaultFactory();
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Failed to parse JSON file at {Path}. Recreating with defaults.", _path);
            var defaults = defaultFactory();
            await WriteInternalAsync(defaults, cancellationToken);
            return defaults;
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task WriteAsync(IEnumerable<T> items, CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            await WriteInternalAsync(items.ToList(), cancellationToken);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task WriteInternalAsync(List<T> items, CancellationToken cancellationToken)
    {
        var tempFile = Path.GetTempFileName();
        await using (var stream = File.Create(tempFile))
        {
            await JsonSerializer.SerializeAsync(stream, items, _serializerOptions, cancellationToken);
        }

        try
        {
            if (File.Exists(_path))
            {
                File.Replace(tempFile, _path, null);
            }
            else
            {
                File.Move(tempFile, _path);
            }
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
