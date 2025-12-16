using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonCharacterRepository : ICharacterRepository
{
    private readonly JsonFileStore<Character> _store;

    public JsonCharacterRepository(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger<JsonCharacterRepository> logger, FileConcurrencyProvider concurrencyProvider)
    {
        _store = new JsonFileStore<Character>(options, environment, logger, concurrencyProvider, options.Value.CharactersFileName);
    }

    public async Task AddAsync(Character character, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<Character>(), cancellationToken);
        characters.Add(character);
        await _store.WriteAsync(characters, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Character>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<Character>(), cancellationToken);
        return characters;
    }

    public async Task<IReadOnlyCollection<Character>> GetByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<Character>(), cancellationToken);
        return characters.Where(c => c.AccountId == accountId).ToList();
    }

    public async Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<Character>(), cancellationToken);
        return characters.FirstOrDefault(c => c.Id == id);
    }

    public async Task UpdateAsync(Character character, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<Character>(), cancellationToken);
        var index = characters.FindIndex(c => c.Id == character.Id);
        if (index < 0)
        {
            characters.Add(character);
        }
        else
        {
            characters[index] = character;
        }

        await _store.WriteAsync(characters, cancellationToken);
    }
}
