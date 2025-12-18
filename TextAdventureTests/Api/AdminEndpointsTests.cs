using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ApplicationServices.Admin.Models;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Database;
using Domain.Enums;
using Domain.ValueObjects;
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

        var userCreateDto = new AdminUserDto(Guid.Empty, "db-admin", "Secret123!", Array.Empty<string>());
        var userCreate = await client.PostAsJsonAsync("/api/admin/users", userCreateDto);
        userCreate.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdUser = await userCreate.Content.ReadFromJsonAsync<AdminUserDto>();
        createdUser.ShouldNotBeNull();
        createdUser!.Id.ShouldNotBe(Guid.Empty);

        var users = await client.GetFromJsonAsync<List<AdminUserDto>>("/api/admin/users");
        users.ShouldContain(u => u.Id == createdUser.Id && u.Username == userCreateDto.Username);

        var updatedUserDto = new AdminUserDto(createdUser.Id, "db-admin-renamed", null, new[] { "token-a", "token-b" });
        var updateUserResponse = await client.PutAsJsonAsync($"/api/admin/users/{createdUser.Id}", updatedUserDto);
        updateUserResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var progressDto = new AdminProgressDto(
            createdUser.Id,
            5,
            100,
            "Exploring",
            DateTimeOffset.UtcNow,
            new List<SaveSlotDto>
            {
                new(
                    Guid.Empty,
                    "Slot 1",
                    5,
                    100,
                    "Exploring",
                    DateTimeOffset.UtcNow,
                    new WorldLocationDto("Forest", "Woodland", "Medium"))
            });

        var progressCreate = await client.PostAsJsonAsync("/api/admin/progress", progressDto);
        progressCreate.StatusCode.ShouldBe(HttpStatusCode.OK);

        var allProgress = await client.GetFromJsonAsync<List<AdminProgressDto>>("/api/admin/progress");
        allProgress.ShouldContain(p => p.UserId == createdUser.Id && p.Level == 5);

        var progressUpdateDto = progressDto with { Level = 6, Experience = 150 };
        var progressUpdate = await client.PutAsJsonAsync($"/api/admin/progress/{createdUser.Id}", progressUpdateDto);
        progressUpdate.StatusCode.ShouldBe(HttpStatusCode.OK);

        var progressDelete = await client.DeleteAsync($"/api/admin/progress/{createdUser.Id}");
        progressDelete.StatusCode.ShouldBe(HttpStatusCode.OK);

        var questDto = new AdminQuestDto(
            "admin_quest",
            "Admin Quest",
            "Test quest",
            "Admin Town",
            null,
            null,
            Array.Empty<string>(),
            50,
            10,
            new List<QuestRewardItemDto> { new("item-id", 1) });

        var questCreate = await client.PostAsJsonAsync("/api/admin/quests", questDto);
        questCreate.StatusCode.ShouldBe(HttpStatusCode.OK);

        var quests = await client.GetFromJsonAsync<List<AdminQuestDto>>("/api/admin/quests");
        quests.ShouldContain(q => q.Id == questDto.Id && q.Name == questDto.Name);

        var questUpdateDto = questDto with { Description = "Updated quest" };
        var questUpdate = await client.PutAsJsonAsync($"/api/admin/quests/{questDto.Id}", questUpdateDto);
        questUpdate.StatusCode.ShouldBe(HttpStatusCode.OK);

        var questDelete = await client.DeleteAsync($"/api/admin/quests/{questDto.Id}");
        questDelete.StatusCode.ShouldBe(HttpStatusCode.OK);

        var townRequest = new TownDto(
            "Admin Town",
            new List<VendorPriceDto> { new("item-id", 5, 2) },
            new List<TownNpcDto>
            {
                new(
                    string.Empty,
                    "Guide",
                    "Helper",
                    NpcRoleType.QuestGiver,
                    false,
                    "Friendly",
                    "Square",
                    new[] { questDto.Id },
                    new NpcDialogueDto(
                        new[] { "Hi" },
                        new[] { "Quest?" },
                        new[] { "Bye" },
                        new[] { "Hmm" },
                        new[] { "Trade" }))
            });

        var townCreate = await client.PostAsJsonAsync("/api/admin/towns", townRequest);
        townCreate.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdTown = await townCreate.Content.ReadFromJsonAsync<TownDto>();
        createdTown.ShouldNotBeNull();
        createdTown!.Npcs.ShouldNotBeEmpty();
        var npcId = createdTown.Npcs.First().Id;

        var towns = await client.GetFromJsonAsync<List<TownDto>>("/api/admin/towns");
        towns.ShouldContain(t => t.Name == townRequest.Name);

        var npcUpdateDto = new TownNpcDto(
            npcId,
            "Guide",
            "Helper",
            NpcRoleType.QuestGiver,
            true,
            "Friendly",
            "Market",
            new[] { questDto.Id },
            new NpcDialogueDto(
                new[] { "Hello" },
                new[] { "Quest?" },
                new[] { "Farewell" },
                new[] { "Chat" },
                new[] { "Browse" }))
        );
        var npcUpdate = await client.PutAsJsonAsync($"/api/admin/towns/{townRequest.Name}/npcs/{npcId}", npcUpdateDto);
        npcUpdate.StatusCode.ShouldBe(HttpStatusCode.OK);

        var locationDto = new AdminWorldLocationDto(
            string.Empty,
            "Admin Ridge",
            "Training grounds",
            "Grassland",
            "Low",
            new List<string>(),
            townRequest.Name);
        var locationCreate = await client.PostAsJsonAsync("/api/admin/locations", locationDto);
        locationCreate.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdLocation = await locationCreate.Content.ReadFromJsonAsync<AdminWorldLocationDto>();
        createdLocation.ShouldNotBeNull();

        var locationUpdate = createdLocation! with { ThreatLevel = "High" };
        var updateLocationResponse = await client.PutAsJsonAsync($"/api/admin/locations/{createdLocation!.Id}", locationUpdate);
        updateLocationResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var dropTable = new DropTableDto("AdminBiome", new List<string> { "item-id" });
        var upsertDrop = await client.PostAsJsonAsync("/api/admin/drop-tables", dropTable);
        upsertDrop.StatusCode.ShouldBe(HttpStatusCode.OK);

        var dropTables = await client.GetFromJsonAsync<List<DropTableDto>>("/api/admin/drop-tables");
        dropTables.ShouldContain(d => d.Biome == dropTable.Biome);

        var dropDelete = await client.DeleteAsync($"/api/admin/drop-tables/{dropTable.Biome}");
        dropDelete.StatusCode.ShouldBe(HttpStatusCode.OK);

        var locationDelete = await client.DeleteAsync($"/api/admin/locations/{createdLocation!.Id}");
        locationDelete.StatusCode.ShouldBe(HttpStatusCode.OK);

        var npcDelete = await client.DeleteAsync($"/api/admin/towns/{townRequest.Name}/npcs/{npcId}");
        npcDelete.StatusCode.ShouldBe(HttpStatusCode.OK);

        var townDelete = await client.DeleteAsync($"/api/admin/towns/{townRequest.Name}");
        townDelete.StatusCode.ShouldBe(HttpStatusCode.OK);

        var deleteUserResponse = await client.DeleteAsync($"/api/admin/users/{createdUser.Id}");
        deleteUserResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
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
                services.RemoveAll<IQuestRepository>();
                services.RemoveAll<IWorldRepository>();

                services.AddSingleton<IGameDatabase, InMemoryGameDatabase>();
                services.AddSingleton<IUserRepository, InMemoryUserRepository>();
                services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
                services.AddSingleton<IQuestRepository, InMemoryQuestRepository>();
                services.AddSingleton<IWorldRepository, InMemoryWorldRepository>();
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

    private class InMemoryQuestRepository : IQuestRepository
    {
        private readonly List<Quest> _quests = DbCreateDefault.World.Quests();

        public Task AddAsync(Quest quest, CancellationToken cancellationToken = default)
        {
            _quests.Add(quest);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            _quests.RemoveAll(q => q.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Quest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<Quest>>(_quests.ToList());
        }

        public Task<Quest?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var quest = _quests.FirstOrDefault(q => q.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(quest);
        }

        public Task UpdateAsync(Quest quest, CancellationToken cancellationToken = default)
        {
            var existing = _quests.FindIndex(q => q.Id.Equals(quest.Id, StringComparison.OrdinalIgnoreCase));
            if (existing < 0)
            {
                _quests.Add(quest);
            }
            else
            {
                _quests[existing] = quest;
            }

            return Task.CompletedTask;
        }
    }

    private class InMemoryWorldRepository : IWorldRepository
    {
        private readonly List<Town> _towns = DbCreateDefault.World.Towns();
        private readonly List<Monster> _monsters = DbCreateDefault.World.Monsters();
        private readonly List<CharacterPreset> _presets = DbCreateDefault.World.CharacterPresets();
        private readonly List<WorldLocationNode> _locations = DbCreateDefault.World.Locations();
        private readonly List<DropTable> _dropTables = DbCreateDefault.World.DropTables();

        public Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<Town>>(_towns.ToList());
        }

        public Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<Monster>>(_monsters.ToList());
        }

        public Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<CharacterPreset>>(_presets.ToList());
        }

        public Task<IReadOnlyCollection<WorldLocationNode>> GetLocationsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<WorldLocationNode>>(_locations.ToList());
        }

        public Task<IReadOnlyCollection<DropTable>> GetDropTablesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<DropTable>>(_dropTables.ToList());
        }

        public Task SaveWorldAsync(
            IEnumerable<Town> towns,
            IEnumerable<Monster> monsters,
            IEnumerable<CharacterPreset> characterPresets,
            IEnumerable<WorldLocationNode> locations,
            IEnumerable<DropTable> dropTables,
            CancellationToken cancellationToken = default)
        {
            UpdateList(_towns, towns, t => t.Name);
            UpdateList(_monsters, monsters, m => m.Id);
            UpdateList(_presets, characterPresets, p => p.Id);
            UpdateList(_locations, locations, l => l.Id);
            UpdateList(_dropTables, dropTables, d => d.Biome);

            return Task.CompletedTask;
        }

        private static void UpdateList<T>(ICollection<T> target, IEnumerable<T> source, Func<T, string> keySelector)
        {
            var incoming = source.ToDictionary(keySelector, value => value, StringComparer.OrdinalIgnoreCase);
            target.Clear();
            foreach (var item in incoming.Values)
            {
                target.Add(item);
            }
        }
    }
}
