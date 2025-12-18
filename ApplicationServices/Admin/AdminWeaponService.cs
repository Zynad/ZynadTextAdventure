using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos;
using Domain.Repos.Weapons;

namespace ApplicationServices.Admin;

public class AdminWeaponService(
    GetCurrentUserHandler getCurrentUserHandler,
    IWandRepository wandRepository,
    IStaffRepository staffRepository,
    ISwordRepository swordRepository,
    IAxeRepository axeRepository)
    : IAdminWeaponService
{
    public async Task<AdminOperationResult<IReadOnlyCollection<WeaponDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<WeaponDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var items = new List<WeaponDto>();
        items.AddRange((await wandRepository.GetAllAsync()).Select(ToDto));
        items.AddRange((await staffRepository.GetAllAsync()).Select(ToDto));
        items.AddRange((await swordRepository.GetAllAsync()).Select(ToDto));
        items.AddRange((await axeRepository.GetAllAsync()).Select(ToDto));

        return AdminOperationResult<IReadOnlyCollection<WeaponDto>>.FromSuccess(items);
    }

    public async Task<AdminOperationResult<WeaponDto>> CreateAsync(string token, WeaponDto weaponDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<WeaponDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(weaponDto.Name))
        {
            return AdminOperationResult<WeaponDto>.ValidationFailed("Name is required");
        }

        return weaponDto.WeaponType switch
        {
            WeaponTypeEntity.Wand => await CreateAsync(wandRepository, weaponDto),
            WeaponTypeEntity.Staff => await CreateAsync(staffRepository, weaponDto),
            WeaponTypeEntity.Sword => await CreateAsync(swordRepository, weaponDto),
            WeaponTypeEntity.Axe => await CreateAsync(axeRepository, weaponDto),
            _ => AdminOperationResult<WeaponDto>.ValidationFailed("Unsupported weapon type")
        };
    }

    public async Task<AdminOperationResult<WeaponDto>> UpdateAsync(string token, WeaponDto weaponDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<WeaponDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        return weaponDto.WeaponType switch
        {
            WeaponTypeEntity.Wand => await UpdateAsync(wandRepository, weaponDto),
            WeaponTypeEntity.Staff => await UpdateAsync(staffRepository, weaponDto),
            WeaponTypeEntity.Sword => await UpdateAsync(swordRepository, weaponDto),
            WeaponTypeEntity.Axe => await UpdateAsync(axeRepository, weaponDto),
            _ => AdminOperationResult<WeaponDto>.ValidationFailed("Unsupported weapon type")
        };
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, WeaponTypeEntity type, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        return type switch
        {
            WeaponTypeEntity.Wand => await DeleteAsync(wandRepository, id),
            WeaponTypeEntity.Staff => await DeleteAsync(staffRepository, id),
            WeaponTypeEntity.Sword => await DeleteAsync(swordRepository, id),
            WeaponTypeEntity.Axe => await DeleteAsync(axeRepository, id),
            _ => AdminOperationResult<bool>.ValidationFailed("Unsupported weapon type")
        };
    }

    private static WeaponDto ToDto(WeaponBaseEntity entity)
    {
        return new WeaponDto(
            entity.Id,
            entity.Name,
            entity.LevelRequirement,
            entity.Rarity,
            entity.Value,
            entity.Weight,
            entity.Durability,
            entity.Material,
            entity.WeaponType,
            entity.MeleeAttackValue,
            entity.RangedAttackValue,
            entity.MagicAttackValue,
            entity.IsRanged,
            entity.TwoHanded,
            entity.Range,
            entity.MagicPower);
    }

    private static TEntity ToEntity<TEntity>(WeaponDto dto) where TEntity : WeaponBaseEntity, new()
    {
        return new TEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            LevelRequirement = dto.LevelRequirement,
            Rarity = dto.Rarity,
            Value = dto.Value,
            Weight = dto.Weight,
            Durability = dto.Durability,
            Material = dto.Material,
            WeaponType = dto.WeaponType,
            MeleeAttackValue = dto.MeleeAttackValue,
            RangedAttackValue = dto.RangedAttackValue,
            MagicAttackValue = dto.MagicAttackValue,
            IsRanged = dto.IsRanged,
            TwoHanded = dto.TwoHanded,
            Range = dto.Range,
            MagicPower = dto.MagicPower
        };
    }

    private static async Task<AdminOperationResult<WeaponDto>> CreateAsync<TEntity>(IBaseRepo<TEntity> repository, WeaponDto dto)
        where TEntity : WeaponBaseEntity, new()
    {
        var entity = ToEntity<TEntity>(dto);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        var persisted = await repository.AddAsync(entity);
        return AdminOperationResult<WeaponDto>.FromSuccess(ToDto(persisted));
    }

    private static async Task<AdminOperationResult<WeaponDto>> UpdateAsync<TEntity>(IBaseRepo<TEntity> repository, WeaponDto dto)
        where TEntity : WeaponBaseEntity, new()
    {
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == dto.Id);
        if (existing is null)
        {
            return AdminOperationResult<WeaponDto>.NotFound("Weapon not found");
        }

        var updated = ToEntity<TEntity>(dto);
        updated.Id = dto.Id;

        var result = await repository.UpdateAsync(updated);
        return AdminOperationResult<WeaponDto>.FromSuccess(ToDto(result));
    }

    private static async Task<AdminOperationResult<bool>> DeleteAsync<TEntity>(IBaseRepo<TEntity> repository, Guid id)
        where TEntity : WeaponBaseEntity
    {
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Weapon not found");
        }

        await repository.DeleteAsync(existing);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
