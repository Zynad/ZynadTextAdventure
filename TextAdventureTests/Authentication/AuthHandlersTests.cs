using ApplicationServices.Authentication;
using ApplicationServices.Authentication.Requests;
using ApplicationServices.Authentication.Results;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Shouldly;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Services;

namespace TextAdventureTests.Authentication;

public class AuthHandlersTests
{
    private readonly InMemoryUserRepository _userRepository = new();
    private readonly InMemorySessionRepository _sessionRepository = new();
    private readonly IAuthService _authService;

    public AuthHandlersTests()
    {
        _authService = new AuthService(new RandomService(), NullLogger<AuthService>.Instance, Options.Create(new AuthOptions
        {
            PasswordPepper = "pepper",
            TokenSecret = "secret",
            TokenExpiryMinutes = 60,
            PasswordIterations = 10_000
        }));
    }

    [Fact]
    public async Task RegisterUserHandler_AddsUserAndSession()
    {
        var handler = new RegisterUserHandler(_userRepository, _sessionRepository, _authService, NullLogger<RegisterUserHandler>.Instance);

        var result = await handler.HandleAsync(new RegisterUserRequest
        {
            Username = "player1",
            Email = "player1@example.com",
            Password = "Password1!"
        });

        result.Success.ShouldBeTrue();
        result.Token.ShouldNotBeNullOrWhiteSpace();
        result.User!.Email.ShouldBe("player1@example.com");

        var stored = await _userRepository.GetByUsernameAsync("player1");
        stored!.Email.ShouldBe("player1@example.com");

        var tokens = await _sessionRepository.GetTokensForAccountAsync(stored.Id);
        tokens.ShouldContain(t => t.Token == result.Token);
    }

    [Fact]
    public async Task RegisterUserHandler_RejectsWeakPassword()
    {
        var handler = new RegisterUserHandler(_userRepository, _sessionRepository, _authService, NullLogger<RegisterUserHandler>.Instance);

        var result = await handler.HandleAsync(new RegisterUserRequest
        {
            Username = "player2",
            Email = "player2@example.com",
            Password = "short"
        });

        result.Success.ShouldBeFalse();
        result.ErrorType.ShouldBe(AuthErrorType.Validation);
    }

    [Fact]
    public async Task LoginUserHandler_IssuesTokenForValidCredentials()
    {
        var loginHandler = new LoginUserHandler(_userRepository, _sessionRepository, _authService, NullLogger<LoginUserHandler>.Instance);
        var registerHandler = new RegisterUserHandler(_userRepository, _sessionRepository, _authService, NullLogger<RegisterUserHandler>.Instance);
        await registerHandler.HandleAsync(new RegisterUserRequest
        {
            Username = "player3",
            Email = "player3@example.com",
            Password = "Password2!"
        });

        var result = await loginHandler.HandleAsync(new LoginUserRequest
        {
            Identifier = "player3",
            Password = "Password2!"
        });

        result.Success.ShouldBeTrue();
        result.Token.ShouldNotBeNull();

        var stored = await _userRepository.GetByUsernameAsync("player3");
        var tokens = await _sessionRepository.GetTokensForAccountAsync(stored!.Id);
        tokens.ShouldContain(t => t.Token == result.Token);
    }

    [Fact]
    public async Task LoginUserHandler_RejectsInvalidPassword()
    {
        var registerHandler = new RegisterUserHandler(_userRepository, _sessionRepository, _authService, NullLogger<RegisterUserHandler>.Instance);
        var loginHandler = new LoginUserHandler(_userRepository, _sessionRepository, _authService, NullLogger<LoginUserHandler>.Instance);

        await registerHandler.HandleAsync(new RegisterUserRequest
        {
            Username = "player4",
            Email = "player4@example.com",
            Password = "Password3!"
        });

        var result = await loginHandler.HandleAsync(new LoginUserRequest
        {
            Identifier = "player4",
            Password = "incorrect"
        });

        result.Success.ShouldBeFalse();
        result.ErrorType.ShouldBe(AuthErrorType.Unauthorized);
    }

    [Fact]
    public async Task GetCurrentUserHandler_ReturnsUnauthorizedForExpiredToken()
    {
        var handler = new GetCurrentUserHandler(_userRepository, _sessionRepository);
        var hash = _authService.HashPassword("Password4!");
        var account = new Account
        {
            Username = "player5",
            Email = "player5@example.com",
            PasswordHash = hash.Hash,
            PasswordSalt = hash.Salt
        };
        await _userRepository.AddAsync(account);

        await _sessionRepository.AddAsync(new SessionToken
        {
            Token = "expired",
            AccountId = account.Id,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(-1)
        });

        var result = await handler.HandleAsync("expired");

        result.Success.ShouldBeFalse();
        result.ErrorType.ShouldBe(AuthErrorType.Unauthorized);
    }

    [Fact]
    public async Task GetCurrentUserHandler_ReturnsUserForValidToken()
    {
        var handler = new GetCurrentUserHandler(_userRepository, _sessionRepository);
        var hash = _authService.HashPassword("Password5!");
        var account = new Account
        {
            Username = "player6",
            Email = "player6@example.com",
            PasswordHash = hash.Hash,
            PasswordSalt = hash.Salt
        };
        await _userRepository.AddAsync(account);

        var session = _authService.CreateSessionToken(account.Id);
        await _sessionRepository.AddAsync(session);

        var result = await handler.HandleAsync(session.Token);

        result.Success.ShouldBeTrue();
        result.User!.Username.ShouldBe("player6");
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
            return Task.FromResult(_accounts.FirstOrDefault(a => a.Sessions.Any(s => s.Token == token)));
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
            var now = DateTimeOffset.UtcNow;
            _tokens.RemoveAll(t => t.ExpiresAt <= now || t.Token == sessionToken.Token);
            _tokens.Add(sessionToken);
            return Task.CompletedTask;
        }

        public Task<SessionToken?> GetValidTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.UtcNow;
            _tokens.RemoveAll(t => t.ExpiresAt <= now);
            return Task.FromResult(_tokens.FirstOrDefault(t => t.Token == token));
        }

        public Task<IReadOnlyCollection<SessionToken>> GetTokensForAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.UtcNow;
            _tokens.RemoveAll(t => t.ExpiresAt <= now);
            return Task.FromResult((IReadOnlyCollection<SessionToken>)_tokens.Where(t => t.AccountId == accountId).ToList());
        }

        public Task RemoveExpiredAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.UtcNow;
            _tokens.RemoveAll(t => t.ExpiresAt <= now);
            return Task.CompletedTask;
        }
    }
}
