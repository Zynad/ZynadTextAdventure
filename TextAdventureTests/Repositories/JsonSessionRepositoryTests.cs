using ApplicationServices.Contracts.Repositories;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Repositories;

namespace TextAdventureTests.Repositories;

public class JsonSessionRepositoryTests : IDisposable
{
    private readonly string _tempDirectory;
    private readonly ISessionRepository _repository;

    public JsonSessionRepositoryTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDirectory);

        var options = Options.Create(new DataStoreOptions
        {
            DataDirectory = _tempDirectory,
            SessionsFileName = "sessions.json"
        });

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_tempDirectory);

        _repository = new JsonSessionRepository(options, environment, NullLogger<JsonSessionRepository>.Instance, new FileConcurrencyProvider());
    }

    [Fact]
    public async Task AddAsync_PersistsToken()
    {
        var token = new SessionToken
        {
            Token = "token-1",
            AccountId = Guid.NewGuid(),
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(5)
        };

        await _repository.AddAsync(token);

        var retrieved = await _repository.GetValidTokenAsync("token-1");
        retrieved.ShouldNotBeNull();
        retrieved!.AccountId.ShouldBe(token.AccountId);
    }

    [Fact]
    public async Task GetValidTokenAsync_RemovesExpiredTokens()
    {
        var expired = new SessionToken
        {
            Token = "old",
            AccountId = Guid.NewGuid(),
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(-2)
        };

        await _repository.AddAsync(expired);

        var result = await _repository.GetValidTokenAsync("old");
        result.ShouldBeNull();

        var tokens = await _repository.GetTokensForAccountAsync(expired.AccountId);
        tokens.ShouldBeEmpty();
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }
        catch
        {
            // best effort
        }
    }
}
