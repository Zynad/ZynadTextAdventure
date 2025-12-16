using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

internal class WorldState
{
    public List<Town> Towns { get; set; } = new();
    public List<Monster> Monsters { get; set; } = new();
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

    public async Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Towns;
    }

    public async Task SaveWorldAsync(IEnumerable<Town> towns, IEnumerable<Monster> monsters, CancellationToken cancellationToken = default)
    {
        var world = new WorldState
        {
            Towns = towns.ToList(),
            Monsters = monsters.ToList()
        };

        await _store.WriteAsync(new[] { world }, cancellationToken);
    }

    private async Task<WorldState> ReadWorldAsync(CancellationToken cancellationToken)
    {
        var data = await _store.ReadAsync(() => new List<WorldState> { new() }, cancellationToken);
        return data.FirstOrDefault() ?? new WorldState();
    }
}
