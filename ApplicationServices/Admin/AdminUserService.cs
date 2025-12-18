using System.Security.Cryptography;
using System.Text;
using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Database;
using Domain.ValueObjects;

namespace ApplicationServices.Admin;

public class AdminUserService(GetCurrentUserHandler getCurrentUserHandler, IGameDatabase gameDatabase) : IAdminUserService
{
    public async Task<AdminOperationResult<IReadOnlyCollection<AdminUserDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<AdminUserDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        var users = database.Users.Select(ToDto).ToList();
        return AdminOperationResult<IReadOnlyCollection<AdminUserDto>>.FromSuccess(users);
    }

    public async Task<AdminOperationResult<AdminUserDto>> CreateAsync(string token, AdminUserDto userDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminUserDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(userDto.Username))
        {
            return AdminOperationResult<AdminUserDto>.ValidationFailed("Username is required");
        }

        if (string.IsNullOrWhiteSpace(userDto.Password))
        {
            return AdminOperationResult<AdminUserDto>.ValidationFailed("Password is required");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        if (database.Users.Any(u => u.Username.Equals(userDto.Username, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<AdminUserDto>.Conflict("User already exists");
        }

        var entity = ToEntity(userDto);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        database.Users.Add(entity);
        await gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<AdminUserDto>.FromSuccess(ToDto(entity));
    }

    public async Task<AdminOperationResult<AdminUserDto>> UpdateAsync(string token, Guid id, AdminUserDto userDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminUserDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(userDto.Username))
        {
            return AdminOperationResult<AdminUserDto>.ValidationFailed("Username is required");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        var existing = database.Users.FirstOrDefault(u => u.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<AdminUserDto>.NotFound("User not found");
        }

        if (database.Users.Any(u => u.Id != id && u.Username.Equals(userDto.Username, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<AdminUserDto>.Conflict("Username already in use");
        }

        existing.Username = userDto.Username;

        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            existing.PasswordHash = HashPassword(userDto.Password);
        }

        if (userDto.SessionTokens is not null)
        {
            existing.SessionTokens = userDto.SessionTokens.ToList();
        }

        await gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<AdminUserDto>.FromSuccess(ToDto(existing));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        var existing = database.Users.FirstOrDefault(u => u.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("User not found");
        }

        database.Users.Remove(existing);
        await gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static AdminUserDto ToDto(UserAccount user)
    {
        return new AdminUserDto(user.Id, user.Username, null, user.SessionTokens.AsReadOnly());
    }

    private static UserAccount ToEntity(AdminUserDto userDto)
    {
        return new UserAccount
        {
            Id = userDto.Id,
            Username = userDto.Username,
            PasswordHash = string.IsNullOrWhiteSpace(userDto.Password) ? string.Empty : HashPassword(userDto.Password),
            SessionTokens = userDto.SessionTokens?.ToList() ?? []
        };
    }

    private static string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
