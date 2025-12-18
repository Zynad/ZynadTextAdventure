using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Entities.Armor.Models;
using Domain.Repos;
using Domain.Repos.Armor;
using System.Linq;

namespace ApplicationServices.Admin;

public class AdminArmorService : IAdminArmorService
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly IReadOnlyDictionary<ArmorSlot, IBaseRepo<ArmorPieceEntity>> _repositories;

    public AdminArmorService(
        GetCurrentUserHandler getCurrentUserHandler,
        IHelmetRepository helmetRepository,
        IChestRepository chestRepository,
        IGlovesRepository glovesRepository,
        ILegsRepository legsRepository,
        IBootsRepository bootsRepository)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _repositories = new Dictionary<ArmorSlot, IBaseRepo<ArmorPieceEntity>>
        {
            [ArmorSlot.Helmet] = helmetRepository,
            [ArmorSlot.Chest] = chestRepository,
            [ArmorSlot.Gloves] = glovesRepository,
            [ArmorSlot.Legs] = legsRepository,
            [ArmorSlot.Boots] = bootsRepository
        };
    }

    public async Task<AdminOperationResult<IReadOnlyCollection<ArmorPieceDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<ArmorPieceDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var items = new List<ArmorPieceDto>();
        foreach (var repository in _repositories)
        {
            var entities = await repository.Value.GetAllAsync();
            items.AddRange(entities.Select(entity => ToDto(entity, repository.Key)));
        }

        return AdminOperationResult<IReadOnlyCollection<ArmorPieceDto>>.FromSuccess(items);
    }

    public async Task<AdminOperationResult<ArmorPieceDto>> CreateAsync(string token, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<ArmorPieceDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(armorPieceDto.Name))
        {
            return AdminOperationResult<ArmorPieceDto>.ValidationFailed("Name is required");
        }

        var repository = _repositories[armorPieceDto.Slot];
        var entity = ToEntity(armorPieceDto);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        var persisted = await repository.AddAsync(entity);
        return AdminOperationResult<ArmorPieceDto>.FromSuccess(ToDto(persisted, armorPieceDto.Slot));
    }

    public async Task<AdminOperationResult<ArmorPieceDto>> UpdateAsync(string token, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<ArmorPieceDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var repository = _repositories[armorPieceDto.Slot];
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == armorPieceDto.Id);
        if (existing is null)
        {
            return AdminOperationResult<ArmorPieceDto>.NotFound("Armor piece not found");
        }

        var updated = ToEntity(armorPieceDto);
        updated.Id = armorPieceDto.Id;
        var result = await repository.UpdateAsync(updated);

        return AdminOperationResult<ArmorPieceDto>.FromSuccess(ToDto(result, armorPieceDto.Slot));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, ArmorSlot slot, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var repository = _repositories[slot];
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Armor piece not found");
        }

        await repository.DeleteAsync(existing);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static ArmorPieceDto ToDto(ArmorPieceEntity entity, ArmorSlot slot)
    {
        return new ArmorPieceDto(
            entity.Id,
            entity.Name,
            entity.LevelRequirement,
            entity.Rarity,
            entity.Value,
            entity.Weight,
            entity.Durability,
            entity.Material,
            entity.PhysicalDefense,
            entity.MagicResistance,
            slot);
    }

    private static ArmorPieceEntity ToEntity(ArmorPieceDto dto)
    {
        return dto.Slot switch
        {
            ArmorSlot.Helmet => new HelmetEntity(),
            ArmorSlot.Chest => new ChestEntity(),
            ArmorSlot.Gloves => new GlovesEntity(),
            ArmorSlot.Legs => new LegsEntity(),
            ArmorSlot.Boots => new BootsEntity(),
            _ => new ArmorPieceEntity()
        }
        {
            Id = dto.Id,
            Name = dto.Name,
            LevelRequirement = dto.LevelRequirement,
            Rarity = dto.Rarity,
            Value = dto.Value,
            Weight = dto.Weight,
            Durability = dto.Durability,
            Material = dto.Material,
            PhysicalDefense = dto.PhysicalDefense,
            MagicResistance = dto.MagicResistance
        };
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
