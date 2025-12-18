using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Database;
using Domain.ValueObjects;

namespace ApplicationServices.Admin;

public class AdminProgressService(GetCurrentUserHandler getCurrentUserHandler, IGameDatabase gameDatabase) : IAdminProgressService
{
    public async Task<AdminOperationResult<IReadOnlyCollection<AdminProgressDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<AdminProgressDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        var progress = database.Progress.Select(ToDto).ToList();
        return AdminOperationResult<IReadOnlyCollection<AdminProgressDto>>.FromSuccess(progress);
    }

    public async Task<AdminOperationResult<AdminProgressDto>> CreateAsync(string token, AdminProgressDto progressDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminProgressDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (progressDto.UserId == Guid.Empty)
        {
            return AdminOperationResult<AdminProgressDto>.ValidationFailed("UserId is required");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        if (database.Progress.Any(p => p.UserId == progressDto.UserId))
        {
            return AdminOperationResult<AdminProgressDto>.Conflict("Progress already exists for user");
        }

        var entity = ToEntity(progressDto);
        if (entity.LastUpdatedUtc == default)
        {
            entity.LastUpdatedUtc = DateTimeOffset.UtcNow;
        }

        database.Progress.Add(entity);
        await gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<AdminProgressDto>.FromSuccess(ToDto(entity));
    }

    public async Task<AdminOperationResult<AdminProgressDto>> UpdateAsync(string token, Guid userId, AdminProgressDto progressDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminProgressDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        var existing = database.Progress.FirstOrDefault(p => p.UserId == userId);
        if (existing is null)
        {
            return AdminOperationResult<AdminProgressDto>.NotFound("Progress not found");
        }

        existing.Level = progressDto.Level;
        existing.Experience = progressDto.Experience;
        existing.AdventureState = progressDto.AdventureState;
        existing.LastUpdatedUtc = progressDto.LastUpdatedUtc == default ? DateTimeOffset.UtcNow : progressDto.LastUpdatedUtc;
        existing.SaveSlots = (progressDto.SaveSlots ?? Array.Empty<SaveSlotDto>()).Select(ToEntity).ToList();

        await gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<AdminProgressDto>.FromSuccess(ToDto(existing));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid userId, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var database = await gameDatabase.ReadAsync(cancellationToken);
        var existing = database.Progress.FirstOrDefault(p => p.UserId == userId);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Progress not found");
        }

        database.Progress.Remove(existing);
        await gameDatabase.WriteAsync(database, cancellationToken);

        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static AdminProgressDto ToDto(PlayerProgress progress)
    {
        return new AdminProgressDto(
            progress.UserId,
            progress.Level,
            progress.Experience,
            progress.AdventureState,
            progress.LastUpdatedUtc,
            progress.SaveSlots.Select(ToDto).ToList());
    }

    private static SaveSlotDto ToDto(SaveSlot saveSlot)
    {
        var location = saveSlot.Location ?? WorldLocation.Default();
        return new SaveSlotDto(
            saveSlot.Id,
            saveSlot.Name,
            saveSlot.Level,
            saveSlot.Experience,
            saveSlot.AdventureState,
            saveSlot.LastUpdatedUtc,
            new WorldLocationDto(location.Name, location.Biome, location.ThreatLevel));
    }

    private static PlayerProgress ToEntity(AdminProgressDto dto)
    {
        return new PlayerProgress
        {
            UserId = dto.UserId,
            Level = dto.Level,
            Experience = dto.Experience,
            AdventureState = dto.AdventureState,
            LastUpdatedUtc = dto.LastUpdatedUtc,
            SaveSlots = (dto.SaveSlots ?? Array.Empty<SaveSlotDto>()).Select(ToEntity).ToList()
        };
    }

    private static SaveSlot ToEntity(SaveSlotDto dto)
    {
        var location = dto.Location ?? new WorldLocationDto(WorldLocation.Default().Name, WorldLocation.Default().Biome, WorldLocation.Default().ThreatLevel);
        return new SaveSlot
        {
            Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            Name = dto.Name,
            Level = dto.Level,
            Experience = dto.Experience,
            AdventureState = dto.AdventureState,
            LastUpdatedUtc = dto.LastUpdatedUtc == default ? DateTimeOffset.UtcNow : dto.LastUpdatedUtc,
            Location = ToEntity(location)
        };
    }

    private static WorldLocation ToEntity(WorldLocationDto dto)
    {
        return new WorldLocation
        {
            Name = dto.Name,
            Biome = dto.Biome,
            ThreatLevel = dto.ThreatLevel
        };
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
