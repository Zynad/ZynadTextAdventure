using System.Linq;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Storage.Mappers;
using TextAdventure.Infrastructure.Storage.Models;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonCharacterRepository : ICharacterRepository
{    
    private readonly JsonFileStore<CharacterModel> _store;

    public JsonCharacterRepository(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger<JsonCharacterRepository> logger, FileConcurrencyProvider concurrencyProvider)
    {
        _store = new JsonFileStore<CharacterModel>(options, environment, logger, concurrencyProvider, options.Value.CharactersFileName);
    }

    public async Task AddAsync(Character character, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<CharacterModel>(), cancellationToken);
        characters.Add(character.ToModel());
        await _store.WriteAsync(characters, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Character>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<CharacterModel>(), cancellationToken);
        return characters.Select(c => c.ToDomain()).ToList();
    }

    public async Task<IReadOnlyCollection<Character>> GetByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<CharacterModel>(), cancellationToken);
        return characters.Where(c => c.AccountId == accountId).Select(c => c.ToDomain()).ToList();
    }

    public async Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<CharacterModel>(), cancellationToken);
        return characters.FirstOrDefault(c => c.Id == id)?.ToDomain();
    }

    public async Task UpdateAsync(Character character, CancellationToken cancellationToken = default)
    {
        var characters = await _store.ReadAsync(() => new List<CharacterModel>(), cancellationToken);
        var index = characters.FindIndex(c => c.Id == character.Id);
        if (index < 0)
        {
            characters.Add(character.ToModel());
        }
        else
        {
            characters[index] = character.ToModel();
        }

        await _store.WriteAsync(characters, cancellationToken);
    }
}
