using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos;
using Domain.Repos.Weapons;
using System.Linq;

namespace ApplicationServices.Admin;

public class AdminWeaponService : IAdminWeaponService
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly IReadOnlyDictionary<WeaponTypeEntity, IBaseRepo<WeaponBaseEntity>> _repositories;

    public AdminWeaponService(
        GetCurrentUserHandler getCurrentUserHandler,
        IWandRepository wandRepository,
        IStaffRepository staffRepository,
        ISwordRepository swordRepository,
        IAxeRepository axeRepository)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _repositories = new Dictionary<WeaponTypeEntity, IBaseRepo<WeaponBaseEntity>>
        {
            [WeaponTypeEntity.Wand] = wandRepository,
            [WeaponTypeEntity.Staff] = staffRepository,
            [WeaponTypeEntity.Sword] = swordRepository,
            [WeaponTypeEntity.Axe] = axeRepository
        };
    }

    public async Task<AdminOperationResult<IReadOnlyCollection<WeaponDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<WeaponDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var items = new List<WeaponDto>();
        foreach (var repository in _repositories)
        {
            var entities = await repository.Value.GetAllAsync();
            items.AddRange(entities.Select(entity => ToDto(entity)));
        }

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

        var repository = _repositories[weaponDto.WeaponType];
        var entity = ToEntity(weaponDto);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        var persisted = await repository.AddAsync(entity);
        return AdminOperationResult<WeaponDto>.FromSuccess(ToDto(persisted));
    }

    public async Task<AdminOperationResult<WeaponDto>> UpdateAsync(string token, WeaponDto weaponDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<WeaponDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var repository = _repositories[weaponDto.WeaponType];
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == weaponDto.Id);
        if (existing is null)
        {
            return AdminOperationResult<WeaponDto>.NotFound("Weapon not found");
        }

        var updated = ToEntity(weaponDto);
        updated.Id = weaponDto.Id;
        var result = await repository.UpdateAsync(updated);
        return AdminOperationResult<WeaponDto>.FromSuccess(ToDto(result));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, WeaponTypeEntity type, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var repository = _repositories[type];
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Weapon not found");
        }

        await repository.DeleteAsync(existing);
        return AdminOperationResult<bool>.FromSuccess(true);
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

    private static WeaponBaseEntity ToEntity(WeaponDto dto)
    {
        var entity = dto.WeaponType switch
        {
            WeaponTypeEntity.Wand => new WandEntity(),
            WeaponTypeEntity.Staff => new StaffEntity(),
            WeaponTypeEntity.Sword => new SwordEntity(),
            WeaponTypeEntity.Axe => new AxeEntity(),
            _ => new WeaponBaseEntity()
        };

        entity.Id = dto.Id;
        entity.Name = dto.Name;
        entity.LevelRequirement = dto.LevelRequirement;
        entity.Rarity = dto.Rarity;
        entity.Value = dto.Value;
        entity.Weight = dto.Weight;
        entity.Durability = dto.Durability;
        entity.Material = dto.Material;
        entity.WeaponType = dto.WeaponType;
        entity.MeleeAttackValue = dto.MeleeAttackValue;
        entity.RangedAttackValue = dto.RangedAttackValue;
        entity.MagicAttackValue = dto.MagicAttackValue;
        entity.IsRanged = dto.IsRanged;
        entity.TwoHanded = dto.TwoHanded;
        entity.Range = dto.Range;
        entity.MagicPower = dto.MagicPower;

        return entity;
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
