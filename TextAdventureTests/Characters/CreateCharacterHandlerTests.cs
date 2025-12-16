using ApplicationServices.Authentication;
using ApplicationServices.Characters;
using ApplicationServices.Characters.Requests;
using ApplicationServices.Characters.Results;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.Entities.Storage;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using TextAdventure.Infrastructure.Services;

namespace TextAdventureTests.Characters;

public class CreateCharacterHandlerTests
{
    private readonly InMemoryUserRepository _users = new();
    private readonly InMemorySessionRepository _sessions = new();
    private readonly InMemoryCharacterRepository _characters = new();
    private readonly FakeWorldRepository _world = new();
    private readonly IAuthService _authService;
    private readonly GetCurrentUserHandler _getCurrentUserHandler;

    public CreateCharacterHandlerTests()
    {
        _authService = new AuthService(new RandomService(), NullLogger<AuthService>.Instance, AuthHelpers.CreateTestOptions());
        _getCurrentUserHandler = new GetCurrentUserHandler(_users, _sessions);
    }

    [Fact]
    public async Task HandleAsync_ReturnsUnauthorized_WhenSessionIsInvalid()
    {
        var handler = CreateHandler();
        var request = new CreateCharacterRequest { Name = "Hero", PresetId = "warrior" };

        var result = await handler.HandleAsync("missing", request);

        result.Success.ShouldBeFalse();
        result.ErrorType.ShouldBe(CharacterErrorType.Unauthorized);
    }

    [Fact]
    public async Task HandleAsync_CreatesCharacterWithPresetInventory()
    {
        var handler = CreateHandler();
        var token = await RegisterUserAsync("player1", "player1@example.com");

        var result = await handler.HandleAsync(token, new CreateCharacterRequest
        {
            Name = "Riza",
            PresetId = "warrior"
        });

        result.Success.ShouldBeTrue();
        result.Character.ShouldNotBeNull();
        result.Character!.Inventory.ShouldNotBeEmpty();

        var stored = await _characters.GetByAccountAsync(result.Character.AccountId);
        stored.ShouldContain(c => c.Name == "Riza");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenNameExistsForAccount()
    {
        var handler = CreateHandler();
        var token = await RegisterUserAsync("player2", "player2@example.com");

        await handler.HandleAsync(token, new CreateCharacterRequest
        {
            Name = "Riza",
            PresetId = "warrior"
        });

        var duplicate = await handler.HandleAsync(token, new CreateCharacterRequest
        {
            Name = "riza",
            PresetId = "warrior"
        });

        duplicate.Success.ShouldBeFalse();
        duplicate.ErrorType.ShouldBe(CharacterErrorType.Conflict);
    }

    private CreateCharacterHandler CreateHandler()
    {
        return new CreateCharacterHandler(_characters, _world, _getCurrentUserHandler, NullLogger<CreateCharacterHandler>.Instance);
    }

    private async Task<string> RegisterUserAsync(string username, string email)
    {
        var passwordHash = _authService.HashPassword("Password1!");
        var account = new Account
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash.Hash,
            PasswordSalt = passwordHash.Salt
        };
        await _users.AddAsync(account);

        var token = _authService.CreateSessionToken(account.Id);
        await _sessions.AddAsync(token);
        return token.Token;
    }

    private class InMemoryCharacterRepository : ICharacterRepository
    {
        private readonly List<Character> _characters = new();

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
        private readonly List<Account> _accounts = new();

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
        private readonly List<SessionToken> _tokens = new();

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

    private class FakeWorldRepository : IWorldRepository
    {
        private readonly IReadOnlyCollection<CharacterPreset> _presets = new List<CharacterPreset>
        {
            new()
            {
                Id = "warrior",
                Name = "Warrior",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "rusty_sword", Quantity = 1 }
                }
            }
        };

        public Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_presets);
        }

        public Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<Town>>(Array.Empty<Town>());
        }

        public Task<IReadOnlyCollection<WorldLocationNode>> GetLocationsAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<WorldLocationNode>>(Array.Empty<WorldLocationNode>());
        }

        public Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyCollection<Monster>>(Array.Empty<Monster>());
        }

        public Task SaveWorldAsync(IEnumerable<Town> towns, IEnumerable<Monster> monsters, IEnumerable<CharacterPreset> characterPresets, IEnumerable<WorldLocationNode> locations, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
