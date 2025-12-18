using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Linq;
using ApplicationServices.Admin.Models;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Database;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;

namespace TextAdventureTests.Api;

public class AdminEndpointsTests
{
    [Fact]
    public async Task AdminEndpoints_RequireAuthentication()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/admin/items");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminEndpoints_SupportCrudFlows()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var token = await RegisterAndLoginAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var monster = new MonsterDto("Sentinel", "Stone guardian", 3, 50, 8);
        var monsterCreate = await client.PostAsJsonAsync("/api/admin/monsters", monster);
        monsterCreate.StatusCode.ShouldBe(HttpStatusCode.OK);

        var monstersResponse = await client.GetFromJsonAsync<List<MonsterDto>>("/api/admin/monsters");
        monstersResponse.ShouldContain(m => m.Name == "Sentinel");

        var updated = monster with { Description = "Awakened sentinel" };
        var updateResponse = await client.PutAsJsonAsync($"/api/admin/monsters/{monster.Name}", updated);
        updateResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var deleteResponse = await client.DeleteAsync($"/api/admin/monsters/{updated.Name}");
        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var item = new ItemDto(Guid.Empty, "Admin Potion", 1, RarityEntity.Common, 10, 1);
        var itemCreate = await client.PostAsJsonAsync("/api/admin/items", item);
        itemCreate.EnsureSuccessStatusCode();
        var createdItem = await itemCreate.Content.ReadFromJsonAsync<ItemDto>();
        createdItem.ShouldNotBeNull();

        var itemUpdate = createdItem! with { Value = 20 };
        var updateItemResponse = await client.PutAsJsonAsync($"/api/admin/items/{createdItem!.Id}", itemUpdate);
        updateItemResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var itemDelete = await client.DeleteAsync($"/api/admin/items/{createdItem!.Id}");
        itemDelete.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    private static async Task<string> RegisterAndLoginAsync(HttpClient client)
    {
        var register = await client.PostAsJsonAsync("/api/auth/register", new
        {
            username = "admin-user",
            email = "admin@example.com",
            password = "Password1!"
        });

        register.EnsureSuccessStatusCode();
        using var registerDoc = JsonDocument.Parse(await register.Content.ReadAsStringAsync());
        var token = registerDoc.RootElement.GetProperty("token").GetString();
        token.ShouldNotBeNull();
        return token!;
    }

    private static WebApplicationFactory<Program> CreateFactory()
    {
        return new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IGameDatabase>();
                services.RemoveAll<IUserRepository>();
                services.RemoveAll<ISessionRepository>();

                services.AddSingleton<IGameDatabase, InMemoryGameDatabase>();
                services.AddSingleton<IUserRepository, InMemoryUserRepository>();
                services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
            });
        });
    }

    private class InMemoryGameDatabase : IGameDatabase
    {
        private DatabaseModel _model = new();

        public Task<DatabaseModel> ReadAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_model);
        }

        public Task WriteAsync(DatabaseModel databaseModel, CancellationToken cancellationToken = default)
        {
            _model = databaseModel;
            return Task.CompletedTask;
        }
    }

    private class InMemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, Account> _users = new();

        public Task AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            _users[account.Id] = account;
            return Task.CompletedTask;
        }

        public Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var account = _users.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(account);
        }

        public Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _users.TryGetValue(id, out var account);
            return Task.FromResult(account);
        }

        public Task<Account?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var account = _users.Values.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(account);
        }

        public Task<Account?> GetBySessionTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            var account = _users.Values.FirstOrDefault(u => u.Sessions.Any(s => s.Token == token && s.ExpiresAt > DateTimeOffset.UtcNow));
            return Task.FromResult(account);
        }

        public Task<IReadOnlyCollection<Account>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<Account> accounts = _users.Values.ToList();
            return Task.FromResult(accounts);
        }

        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            _users[account.Id] = account;
            return Task.CompletedTask;
        }
    }

    private class InMemorySessionRepository : ISessionRepository
    {
        private readonly List<Domain.ValueObjects.SessionToken> _tokens = [];

        public Task AddAsync(Domain.ValueObjects.SessionToken sessionToken, CancellationToken cancellationToken = default)
        {
            _tokens.Add(sessionToken);
            return Task.CompletedTask;
        }

        public Task<Domain.ValueObjects.SessionToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            var session = _tokens.FirstOrDefault(t => t.Token == token && t.ExpiresAt > DateTimeOffset.UtcNow);
            return Task.FromResult(session);
        }

        public Task<IReadOnlyCollection<Domain.ValueObjects.SessionToken>> GetTokensForAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var tokens = _tokens.Where(t => t.AccountId == accountId).ToList();
            return Task.FromResult<IReadOnlyCollection<Domain.ValueObjects.SessionToken>>(tokens);
        }

        public Task RemoveExpiredAsync(CancellationToken cancellationToken = default)
        {
            _tokens.RemoveAll(t => t.ExpiresAt <= DateTimeOffset.UtcNow);
            return Task.CompletedTask;
        }
    }
}
