using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonUserRepository : IUserRepository
{
    private readonly JsonFileStore<Account> _store;
    private readonly ILogger<JsonUserRepository> _logger;

    public JsonUserRepository(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger<JsonUserRepository> logger, FileConcurrencyProvider concurrencyProvider)
    {
        _store = new JsonFileStore<Account>(options, environment, logger, concurrencyProvider, options.Value.AccountsFileName);
        _logger = logger;
    }

    public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        var accounts = await _store.ReadAsync(() => new List<Account>(), cancellationToken);
        accounts.Add(account);
        await _store.WriteAsync(accounts, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Account>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accounts = await _store.ReadAsync(() => new List<Account>(), cancellationToken);
        return accounts;
    }

    public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accounts = await _store.ReadAsync(() => new List<Account>(), cancellationToken);
        return accounts.FirstOrDefault(a => a.Id == id);
    }

    public async Task<Account?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var accounts = await _store.ReadAsync(() => new List<Account>(), cancellationToken);
        return accounts.FirstOrDefault(a => string.Equals(a.Username, username, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Account?> GetBySessionTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var accounts = await _store.ReadAsync(() => new List<Account>(), cancellationToken);
        return accounts.FirstOrDefault(a => a.Sessions.Any(s => s.Token == token && s.ExpiresAt > DateTimeOffset.UtcNow));
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        var accounts = await _store.ReadAsync(() => new List<Account>(), cancellationToken);
        var index = accounts.FindIndex(a => a.Id == account.Id);
        if (index < 0)
        {
            _logger.LogWarning("Attempted to update unknown account {AccountId}", account.Id);
            accounts.Add(account);
        }
        else
        {
            accounts[index] = account;
        }

        await _store.WriteAsync(accounts, cancellationToken);
    }
}
