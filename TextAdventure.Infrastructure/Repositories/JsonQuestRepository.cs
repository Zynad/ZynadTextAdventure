using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonQuestRepository(
    IOptions<DataStoreOptions> options,
    IHostEnvironment environment,
    ILogger<JsonQuestRepository> logger,
    FileConcurrencyProvider concurrencyProvider)
    : IQuestRepository
{
    private readonly JsonFileStore<Quest> _store = new(options, environment, logger, concurrencyProvider, options.Value.QuestsFileName);

    public async Task AddAsync(Quest quest, CancellationToken cancellationToken = default)
    {
        var quests = await ReadQuestsAsync(cancellationToken);
        quests.Add(quest);
        await _store.WriteAsync(quests, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Quest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var quests = await ReadQuestsAsync(cancellationToken);
        return quests;
    }

    public async Task<Quest?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var quests = await ReadQuestsAsync(cancellationToken);
        return quests.FirstOrDefault(q => string.Equals(q.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    public async Task UpdateAsync(Quest quest, CancellationToken cancellationToken = default)
    {
        var quests = await ReadQuestsAsync(cancellationToken);
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

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var quests = await ReadQuestsAsync(cancellationToken);
        var removed = quests.RemoveAll(q => string.Equals(q.Id, id, StringComparison.OrdinalIgnoreCase)) > 0;
        if (removed)
        {
            await _store.WriteAsync(quests, cancellationToken);
        }
    }

    private Task<List<Quest>> ReadQuestsAsync(CancellationToken cancellationToken)
    {
        return _store.ReadAsync(DbCreateDefault.World.Quests, cancellationToken);
    }
}
