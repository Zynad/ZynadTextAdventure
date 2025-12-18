using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Database;
using Domain.ValueObjects;
using System.Linq;

namespace ApplicationServices.Admin;

public class AdminMonsterService : IAdminMonsterService
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly IGameDatabase _gameDatabase;

    public AdminMonsterService(GetCurrentUserHandler getCurrentUserHandler, IGameDatabase gameDatabase)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _gameDatabase = gameDatabase;
    }

    public async Task<AdminOperationResult<IReadOnlyCollection<MonsterDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<MonsterDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await _gameDatabase.ReadAsync(cancellationToken);
        var monsters = database.Monsters.Select(ToDto).ToList();
        return AdminOperationResult<IReadOnlyCollection<MonsterDto>>.FromSuccess(monsters);
    }

    public async Task<AdminOperationResult<MonsterDto>> CreateAsync(string token, MonsterDto monsterDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<MonsterDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(monsterDto.Name))
        {
            return AdminOperationResult<MonsterDto>.ValidationFailed("Name is required");
        }

        var database = await _gameDatabase.ReadAsync(cancellationToken);
        if (database.Monsters.Any(m => m.Name.Equals(monsterDto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<MonsterDto>.Conflict("Monster already exists");
        }

        var entity = ToEntity(monsterDto);
        database.Monsters.Add(entity);
        await _gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<MonsterDto>.FromSuccess(monsterDto);
    }

    public async Task<AdminOperationResult<MonsterDto>> UpdateAsync(string token, string name, MonsterDto monsterDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<MonsterDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await _gameDatabase.ReadAsync(cancellationToken);
        var existing = database.Monsters.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (existing is null)
        {
            return AdminOperationResult<MonsterDto>.NotFound("Monster not found");
        }

        existing.Name = monsterDto.Name;
        existing.Description = monsterDto.Description;
        existing.Level = monsterDto.Level;
        existing.HitPoints = monsterDto.HitPoints;
        existing.AttackPower = monsterDto.AttackPower;

        await _gameDatabase.WriteAsync(database, cancellationToken);
        return AdminOperationResult<MonsterDto>.FromSuccess(ToDto(existing));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, string name, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await _gameDatabase.ReadAsync(cancellationToken);
        var existing = database.Monsters.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Monster not found");
        }

        database.Monsters.Remove(existing);
        await _gameDatabase.WriteAsync(database, cancellationToken);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static MonsterDto ToDto(MonsterProfile monster)
    {
        return new MonsterDto(monster.Name, monster.Description, monster.Level, monster.HitPoints, monster.AttackPower);
    }

    private static MonsterProfile ToEntity(MonsterDto dto)
    {
        return new MonsterProfile
        {
            Name = dto.Name,
            Description = dto.Description,
            Level = dto.Level,
            HitPoints = dto.HitPoints,
            AttackPower = dto.AttackPower
        };
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
