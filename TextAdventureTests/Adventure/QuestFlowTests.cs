using ApplicationServices.Adventure;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Authentication;
using ApplicationServices.Characters;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using TextAdventure.Infrastructure.Services;

namespace TextAdventureTests.Adventure;

public class QuestFlowTests
{
    private readonly InMemoryUserRepository _users = new();
    private readonly InMemorySessionRepository _sessions = new();
    private readonly InMemoryQuestRepository _quests = new();
    private readonly InMemoryCharacterRepository _characters = new();
    private readonly InMemoryWorldRepository _world = new();
    private readonly IAuthService _authService;
    private readonly GetCurrentUserHandler _currentUserHandler;

    public QuestFlowTests()
    {
        _authService = new AuthService(new RandomService(), NullLogger<AuthService>.Instance, AuthHelpers.CreateTestOptions());
        _currentUserHandler = new GetCurrentUserHandler(_users, _sessions);
    }

    [Fact]
    public async Task AcceptQuestHandler_ValidatesLocationAndPrerequisites()
    {
        var token = await RegisterUserAsync();
        var character = await CreateCharacterAsync(token, "Emberbrook Gate");
        _quests.Add(new Quest
        {
            Id = "help_the_blacksmith",
            AcceptLocationId = "emberbrook_gate",
            PrerequisiteQuestIds = ["intro"]
        });

        character.QuestStates.Add(new CharacterQuestState
        {
            QuestId = "intro",
            Status = QuestProgressStatus.Completed
        });

        var handler = new AcceptQuestHandler(_currentUserHandler, _quests, _characters, _world, NullLogger<AcceptQuestHandler>.Instance);
        var result = await handler.HandleAsync(token, "help_the_blacksmith", new QuestActionRequest
        {
            CharacterId = character.Id
        });

        result.Success.ShouldBeTrue();
        character.QuestStates.ShouldContain(q => q.QuestId == "help_the_blacksmith" && q.Status == QuestProgressStatus.Accepted);
    }

    [Fact]
    public async Task CompleteQuestHandler_GrantsRewardsAndMarksComplete()
    {
        var token = await RegisterUserAsync();
        var character = await CreateCharacterAsync(token, "Emberbrook Square");
        var quest = new Quest
        {
            Id = "help_the_blacksmith",
            CompletionLocationId = "emberbrook_square",
            ExperienceReward = 220,
            CoinReward = 40,
            RewardItems = [new() { ItemId = "tempered_sword", Quantity = 1 }]
        };
        _quests.Add(quest);

        character.QuestStates.Add(new CharacterQuestState
        {
            QuestId = quest.Id,
            Status = QuestProgressStatus.Accepted
        });

        var handler = new CompleteQuestHandler(_currentUserHandler, _quests, _characters, _world, NullLogger<CompleteQuestHandler>.Instance);
        var result = await handler.HandleAsync(token, quest.Id, new QuestActionRequest { CharacterId = character.Id });

        result.Success.ShouldBeTrue();
        character.QuestStates.ShouldContain(q => q.QuestId == quest.Id && q.Status == QuestProgressStatus.Completed);
        character.Inventory.ShouldContain(i => i.ItemId == "tempered_sword" && i.Quantity == 1);
        character.Experience.ShouldBe(quest.ExperienceReward);
        character.Coins.ShouldBe(20 + quest.CoinReward);
    }

    [Fact]
    public async Task TravelToLocation_AppliesEncounterRewards()
    {
        var token = await RegisterUserAsync();
        var character = await CreateCharacterAsync(token, "Emberbrook Gate");

        var monster = new Monster
        {
            Id = "ember_wolf",
            Name = "Ember Wolf",
            Biome = "Village",
            LevelRange = new MonsterStatRange { Min = 1, Max = 2 },
            HitPointRange = new MonsterStatRange { Min = 6, Max = 8 },
            AttackRange = new MonsterStatRange { Min = 1, Max = 2 },
            DefenseRange = new MonsterStatRange { Min = 0, Max = 1 },
            CoinDropRange = new MonsterStatRange { Min = 2, Max = 2 },
            PreferredThreatLevels = ["Low"]
        };

        _world.Monsters.Add(monster);
        _world.DropTables.Add(new DropTable { Biome = "Village", Drops = ["wolf_pelt"] });

        var random = new FixedRandomService();
        var encounterGenerator = new EncounterGenerator(random, _world, NullLogger<EncounterGenerator>.Instance);
        var handler = new TravelToLocationHandler(
            _currentUserHandler,
            _characters,
            _world,
            encounterGenerator,
            NullLogger<TravelToLocationHandler>.Instance);

        var result = await handler.HandleAsync(token, new TravelRequest
        {
            CharacterId = character.Id,
            DestinationId = "emberbrook_square"
        });

        result.Success.ShouldBeTrue();
        character.Experience.ShouldBeGreaterThan(0);
        character.Coins.ShouldBeGreaterThan(20);
        character.Inventory.ShouldContain(i => i.ItemId == "wolf_pelt");
        character.EncounterLog.ShouldNotBeEmpty();
    }

    private async Task<Character> CreateCharacterAsync(string token, string locationName)
    {
        var handler = new CreateCharacterHandler(_characters, _world, _currentUserHandler, NullLogger<CreateCharacterHandler>.Instance);
        var result = await handler.HandleAsync(token, new CreateCharacterRequestDto
        {
            Name = Guid.NewGuid().ToString(),
            PresetId = "warrior"
        });

        var character = _characters.AllCharacters.Single();
        character.Location = new WorldLocation { Name = locationName, Biome = "Village", ThreatLevel = "Low" };
        await _characters.UpdateAsync(character);
        return character;
    }

    private async Task<string> RegisterUserAsync()
    {
        var passwordHash = _authService.HashPassword("Password1!");
        var account = new Account
        {
            Username = "player1",
            Email = "player1@example.com",
            PasswordHash = passwordHash.Hash,
            PasswordSalt = passwordHash.Salt
        };
        await _users.AddAsync(account);

        var token = _authService.CreateSessionToken(account.Id);
        await _sessions.AddAsync(token);
        return token.Token;
    }

    private sealed class FixedRandomService : IRandomService
    {
        public int NextInt(int minInclusive, int maxExclusive) => minInclusive;

        public double NextDouble() => 0.1;

        public byte[] GetBytes(int length) => Enumerable.Repeat((byte)1, length).ToArray();
    }

    private class InMemoryQuestRepository : IQuestRepository
    {
        private readonly List<Quest> _quests = [];

        public void Add(Quest quest) => _quests.Add(quest);

        public Task AddAsync(Quest quest, CancellationToken cancellationToken = default)
        {
            _quests.Add(quest);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Quest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyCollection<Quest>)_quests.ToList());
        }

        public Task<Quest?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_quests.FirstOrDefault(q => q.Id.Equals(id, StringComparison.OrdinalIgnoreCase)));
        }

        public Task UpdateAsync(Quest quest, CancellationToken cancellationToken = default)
        {
            var index = _quests.FindIndex(q => q.Id.Equals(quest.Id, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                _quests[index] = quest;
            }
            return Task.CompletedTask;
        }
    }

    private class InMemoryCharacterRepository : ICharacterRepository
    {
        private readonly List<Character> _characters = [];

        public IReadOnlyCollection<Character> AllCharacters => _characters;

        public Task AddAsync(Character character, CancellationToken cancellationToken = default)
        {
            _characters.Add(character);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Character>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyCollection<Character>)_characters.ToList());
        }

        public Task<IReadOnlyCollection<Character>> GetByAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyCollection<Character>)_characters.Where(c => c.AccountId == accountId).ToList());
        }

        public Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_characters.FirstOrDefault(c => c.Id == id));
        }

        public Task UpdateAsync(Character character, CancellationToken cancellationToken = default)
        {
            var index = _characters.FindIndex(c => c.Id == character.Id);
            if (index >= 0)
            {
                _characters[index] = character;
            }
            return Task.CompletedTask;
        }
    }

    private class InMemoryUserRepository : IUserRepository
    {
        private readonly List<Account> _accounts = [];

        public Task AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            _accounts.Add(account);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Account>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyCollection<Account>)_accounts.ToList());
        }

        public Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_accounts.FirstOrDefault(a => string.Equals(a.Email, email, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_accounts.FirstOrDefault(a => a.Id == id));
        }

        public Task<Account?> GetBySessionTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_accounts.FirstOrDefault(a => a.Sessions.Any(s => s.Token == token && s.ExpiresAt > DateTimeOffset.UtcNow)));
        }

        public Task<Account?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_accounts.FirstOrDefault(a => string.Equals(a.Username, username, StringComparison.OrdinalIgnoreCase)));
        }

        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            var index = _accounts.FindIndex(a => a.Id == account.Id);
            if (index >= 0)
            {
                _accounts[index] = account;
            }

            return Task.CompletedTask;
        }
    }

    private class InMemorySessionRepository : ISessionRepository
    {
        private readonly List<SessionToken> _tokens = [];

        public Task AddAsync(SessionToken sessionToken, CancellationToken cancellationToken = default)
        {
            _tokens.RemoveAll(t => t.ExpiresAt <= DateTimeOffset.UtcNow || t.Token == sessionToken.Token);
            _tokens.Add(sessionToken);
            return Task.CompletedTask;
        }

        public Task<SessionToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            _tokens.RemoveAll(t => t.ExpiresAt <= DateTimeOffset.UtcNow);
            return Task.FromResult(_tokens.FirstOrDefault(t => t.Token == token));
        }

        public Task<IReadOnlyCollection<SessionToken>> GetTokensForAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            _tokens.RemoveAll(t => t.ExpiresAt <= DateTimeOffset.UtcNow);
            return Task.FromResult((IReadOnlyCollection<SessionToken>)_tokens.Where(t => t.AccountId == accountId).ToList());
        }

        public Task RemoveExpiredAsync(CancellationToken cancellationToken = default)
        {
            _tokens.RemoveAll(t => t.ExpiresAt <= DateTimeOffset.UtcNow);
            return Task.CompletedTask;
        }
    }

    private class InMemoryWorldRepository : IWorldRepository
    {
        public List<Monster> Monsters { get; } = [];
        public List<DropTable> DropTables { get; } = [];

        private readonly IReadOnlyCollection<WorldLocationNode> _locations = new List<WorldLocationNode>
        {
            new()
            {
                Id = "emberbrook_gate",
                Name = "Emberbrook Gate",
                Biome = "Village",
                ThreatLevel = "Low",
                AdjacentLocationIds = ["emberbrook_square"]
            },
            new()
            {
                Id = "emberbrook_square",
                Name = "Emberbrook Square",
                Biome = "Village",
                ThreatLevel = "Low",
                AdjacentLocationIds = ["emberbrook_gate"]
            }
        };

        public Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyCollection<Monster>)Monsters);
        }

        public Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<CharacterPreset>>(new List<CharacterPreset>
            {
                new()
                {
                    Id = "warrior",
                    Name = "Warrior",
                    StartingLocation = WorldLocation.Default(),
                    StartingInventory = []
                }
            });
        }

        public Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<Town>>([]);
        }

        public Task<IReadOnlyCollection<DropTable>> GetDropTablesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyCollection<DropTable>)DropTables);
        }

        public Task<IReadOnlyCollection<WorldLocationNode>> GetLocationsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_locations);
        }

        public Task SaveWorldAsync(
            IEnumerable<Town> towns,
            IEnumerable<Monster> monsters,
            IEnumerable<CharacterPreset> characterPresets,
            IEnumerable<WorldLocationNode> locations,
            IEnumerable<DropTable> dropTables,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
