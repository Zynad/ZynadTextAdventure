using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Services;

public interface IAuthService
{
    PasswordHash HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash, string passwordSalt);
    SessionToken CreateSessionToken(Guid accountId);
}
