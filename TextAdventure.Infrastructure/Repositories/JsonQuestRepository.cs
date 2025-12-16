using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonQuestRepository : IQuestRepository
{
    private readonly JsonFileStore<Quest> _store;

    public JsonQuestRepository(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger<JsonQuestRepository> logger, FileConcurrencyProvider concurrencyProvider)
    {
        _store = new JsonFileStore<Quest>(options, environment, logger, concurrencyProvider, options.Value.QuestsFileName);
    }

    public async Task AddAsync(Quest quest, CancellationToken cancellationToken = default)
    {
        var quests = await _store.ReadAsync(() => new List<Quest>(), cancellationToken);
        quests.Add(quest);
        await _store.WriteAsync(quests, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Quest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var quests = await _store.ReadAsync(() => new List<Quest>(), cancellationToken);
        return quests;
    }

    public async Task<Quest?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var quests = await _store.ReadAsync(() => new List<Quest>(), cancellationToken);
        return quests.FirstOrDefault(q => string.Equals(q.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    public async Task UpdateAsync(Quest quest, CancellationToken cancellationToken = default)
    {
        var quests = await _store.ReadAsync(() => new List<Quest>(), cancellationToken);
        var index = quests.FindIndex(q => string.Equals(q.Id, quest.Id, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
        {
            quests.Add(quest);
        }
        else
        {
            quests[index] = quest;
        }

        await _store.WriteAsync(quests, cancellationToken);
    }
}
