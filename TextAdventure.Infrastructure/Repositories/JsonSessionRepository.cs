using ApplicationServices.Contracts.Repositories;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonSessionRepository(
    IOptions<DataStoreOptions> options,
    IHostEnvironment environment,
    ILogger<JsonSessionRepository> logger,
    FileConcurrencyProvider concurrencyProvider)
    : ISessionRepository
{
    private readonly JsonFileStore<SessionToken> _store = new(options, environment, logger, concurrencyProvider, options.Value.SessionsFileName);

    public async Task AddAsync(SessionToken sessionToken, CancellationToken cancellationToken = default)
    {
        var tokens = await _store.ReadAsync(() => [], cancellationToken);
        var now = DateTimeOffset.UtcNow;
        tokens.RemoveAll(t => t.ExpiresAt <= now || t.Token == sessionToken.Token);
        tokens.Add(sessionToken);
        await _store.WriteAsync(tokens, cancellationToken);
    }

    public async Task<SessionToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var tokens = await _store.ReadAsync(() => [], cancellationToken);
        var now = DateTimeOffset.UtcNow;
        var validTokens = tokens.Where(t => t.ExpiresAt > now).ToList();

        if (validTokens.Count != tokens.Count)
        {
            await _store.WriteAsync(validTokens, cancellationToken);
        }

        return validTokens.FirstOrDefault(t => t.Token == token);
    }

    public async Task<IReadOnlyCollection<SessionToken>> GetTokensForAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var tokens = await _store.ReadAsync(() => [], cancellationToken);
        var now = DateTimeOffset.UtcNow;
        var validTokens = tokens.Where(t => t.ExpiresAt > now).ToList();

        if (validTokens.Count != tokens.Count)
        {
            await _store.WriteAsync(validTokens, cancellationToken);
        }

        return validTokens.Where(t => t.AccountId == accountId).ToList();
    }

    public async Task RemoveExpiredAsync(CancellationToken cancellationToken = default)
    {
        var tokens = await _store.ReadAsync(() => [], cancellationToken);
        var now = DateTimeOffset.UtcNow;
        var validTokens = tokens.Where(t => t.ExpiresAt > now).ToList();
        if (validTokens.Count != tokens.Count)
        {
            await _store.WriteAsync(validTokens, cancellationToken);
        }
    }
}
