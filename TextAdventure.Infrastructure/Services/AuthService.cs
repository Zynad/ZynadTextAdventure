using System.Security.Cryptography;
using System.Text;
using ApplicationServices.Contracts.Services;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Services;

public class AuthService(IRandomService randomService, ILogger<AuthService> logger, IOptions<AuthOptions> options)
    : IAuthService
{
    private readonly AuthOptions _options = options.Value;

    public SessionToken CreateSessionToken(Guid accountId)
    {
        var randomBytes = randomService.GetBytes(32);
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

    public PasswordHash HashPassword(string password)
    {
        var salt = randomService.GetBytes(16);
        var hash = DeriveHash(password, salt);

        return new PasswordHash
        {
            Hash = Convert.ToBase64String(hash),
            Salt = Convert.ToBase64String(salt)
        };
    }

    public bool VerifyPassword(string password, string passwordHash, string passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(passwordSalt) || string.IsNullOrWhiteSpace(passwordHash))
        {
            logger.LogWarning("Account missing password details");
            return false;
        }

        var salt = Convert.FromBase64String(passwordSalt);
        var computedHash = DeriveHash(password, salt);
        var storedHash = Convert.FromBase64String(passwordHash);
        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }

    private byte[] DeriveHash(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password + _options.PasswordPepper);
        return Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, _options.PasswordIterations, HashAlgorithmName.SHA256, 32);
    }
}
