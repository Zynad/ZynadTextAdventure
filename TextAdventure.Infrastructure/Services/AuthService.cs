using System.Security.Cryptography;
using System.Text;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRandomService _randomService;
    private readonly ILogger<AuthService> _logger;
    private readonly AuthOptions _options;

    public AuthService(IUserRepository userRepository, IRandomService randomService, ILogger<AuthService> logger, IOptions<AuthOptions> options)
    {
        _userRepository = userRepository;
        _randomService = randomService;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<SessionToken?> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var account = await _userRepository.GetByUsernameAsync(username, cancellationToken);
        if (account is null)
        {
            return null;
        }

        if (!VerifyPassword(account, password))
        {
            return null;
        }

        var sessionToken = CreateSessionToken(account.Id);
        account.Sessions.RemoveAll(s => s.ExpiresAt <= DateTimeOffset.UtcNow);
        account.Sessions.Add(sessionToken);
        await _userRepository.UpdateAsync(account, cancellationToken);

        return sessionToken;
    }

    public async Task<Account?> RegisterAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var existing = await _userRepository.GetByUsernameAsync(username, cancellationToken);
        if (existing is not null)
        {
            return null;
        }

        var salt = _randomService.GetBytes(16);
        var hash = DeriveHash(password, salt);

        var account = new Account
        {
            Username = username,
            PasswordHash = Convert.ToBase64String(hash),
            PasswordSalt = Convert.ToBase64String(salt),
            Sessions = new List<SessionToken>()
        };

        await _userRepository.AddAsync(account, cancellationToken);
        return account;
    }

    public async Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var account = await _userRepository.GetBySessionTokenAsync(token, cancellationToken);
        return account is not null;
    }

    private SessionToken CreateSessionToken(Guid accountId)
    {
        var randomBytes = _randomService.GetBytes(32);
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.TokenSecret));
        var signature = hmac.ComputeHash(randomBytes);
        var tokenPayload = Convert.ToBase64String(randomBytes);
        var tokenSignature = Convert.ToBase64String(signature);

        return new SessionToken
        {
            Token = $"{tokenPayload}.{tokenSignature}",
            AccountId = accountId,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(_options.TokenExpiryMinutes)
        };
    }

    private byte[] DeriveHash(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password + _options.PasswordPepper);
        return Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, _options.PasswordIterations, HashAlgorithmName.SHA256, 32);
    }

    private bool VerifyPassword(Account account, string password)
    {
        if (string.IsNullOrWhiteSpace(account.PasswordSalt) || string.IsNullOrWhiteSpace(account.PasswordHash))
        {
            _logger.LogWarning("Account {AccountId} missing password details", account.Id);
            return false;
        }

        var salt = Convert.FromBase64String(account.PasswordSalt);
        var computedHash = DeriveHash(password, salt);
        var storedHash = Convert.FromBase64String(account.PasswordHash);
        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }
}
