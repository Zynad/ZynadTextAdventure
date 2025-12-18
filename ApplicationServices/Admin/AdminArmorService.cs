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
    private readonly IHelmetRepository _helmetRepository;
    private readonly IChestRepository _chestRepository;
    private readonly IGlovesRepository _glovesRepository;
    private readonly ILegsRepository _legsRepository;
    private readonly IBootsRepository _bootsRepository;

    public AdminArmorService(
        GetCurrentUserHandler getCurrentUserHandler,
        IHelmetRepository helmetRepository,
        IChestRepository chestRepository,
        IGlovesRepository glovesRepository,
        ILegsRepository legsRepository,
        IBootsRepository bootsRepository)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _helmetRepository = helmetRepository;
        _chestRepository = chestRepository;
        _glovesRepository = glovesRepository;
        _legsRepository = legsRepository;
        _bootsRepository = bootsRepository;
    }

    public async Task<AdminOperationResult<IReadOnlyCollection<ArmorPieceDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<ArmorPieceDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var items = new List<ArmorPieceDto>();
        items.AddRange((await _helmetRepository.GetAllAsync()).Select(entity => ToDto(entity, ArmorSlot.Helmet)));
        items.AddRange((await _chestRepository.GetAllAsync()).Select(entity => ToDto(entity, ArmorSlot.Chest)));
        items.AddRange((await _glovesRepository.GetAllAsync()).Select(entity => ToDto(entity, ArmorSlot.Gloves)));
        items.AddRange((await _legsRepository.GetAllAsync()).Select(entity => ToDto(entity, ArmorSlot.Legs)));
        items.AddRange((await _bootsRepository.GetAllAsync()).Select(entity => ToDto(entity, ArmorSlot.Boots)));

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

        return armorPieceDto.Slot switch
        {
            ArmorSlot.Helmet => await CreateAsync(_helmetRepository, armorPieceDto, ArmorSlot.Helmet),
            ArmorSlot.Chest => await CreateAsync(_chestRepository, armorPieceDto, ArmorSlot.Chest),
            ArmorSlot.Gloves => await CreateAsync(_glovesRepository, armorPieceDto, ArmorSlot.Gloves),
            ArmorSlot.Legs => await CreateAsync(_legsRepository, armorPieceDto, ArmorSlot.Legs),
            ArmorSlot.Boots => await CreateAsync(_bootsRepository, armorPieceDto, ArmorSlot.Boots),
            _ => AdminOperationResult<ArmorPieceDto>.ValidationFailed("Unsupported armor slot")
        };
    }

    public async Task<AdminOperationResult<ArmorPieceDto>> UpdateAsync(string token, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<ArmorPieceDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        return armorPieceDto.Slot switch
        {
            ArmorSlot.Helmet => await UpdateAsync(_helmetRepository, armorPieceDto, ArmorSlot.Helmet),
            ArmorSlot.Chest => await UpdateAsync(_chestRepository, armorPieceDto, ArmorSlot.Chest),
            ArmorSlot.Gloves => await UpdateAsync(_glovesRepository, armorPieceDto, ArmorSlot.Gloves),
            ArmorSlot.Legs => await UpdateAsync(_legsRepository, armorPieceDto, ArmorSlot.Legs),
            ArmorSlot.Boots => await UpdateAsync(_bootsRepository, armorPieceDto, ArmorSlot.Boots),
            _ => AdminOperationResult<ArmorPieceDto>.ValidationFailed("Unsupported armor slot")
        };
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, ArmorSlot slot, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        return slot switch
        {
            ArmorSlot.Helmet => await DeleteAsync(_helmetRepository, id),
            ArmorSlot.Chest => await DeleteAsync(_chestRepository, id),
            ArmorSlot.Gloves => await DeleteAsync(_glovesRepository, id),
            ArmorSlot.Legs => await DeleteAsync(_legsRepository, id),
            ArmorSlot.Boots => await DeleteAsync(_bootsRepository, id),
            _ => AdminOperationResult<bool>.ValidationFailed("Unsupported armor slot")
        };
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

    private static TEntity ToEntity<TEntity>(ArmorPieceDto dto) where TEntity : ArmorPieceEntity, new()
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
            PhysicalDefense = dto.PhysicalDefense,
            MagicResistance = dto.MagicResistance
        };
    }

    private static async Task<AdminOperationResult<ArmorPieceDto>> CreateAsync<TEntity>(IBaseRepo<TEntity> repository, ArmorPieceDto dto, ArmorSlot slot)
        where TEntity : ArmorPieceEntity, new()
    {
        var entity = ToEntity<TEntity>(dto);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        var persisted = await repository.AddAsync(entity);
        return AdminOperationResult<ArmorPieceDto>.FromSuccess(ToDto(persisted, slot));
    }

    private static async Task<AdminOperationResult<ArmorPieceDto>> UpdateAsync<TEntity>(IBaseRepo<TEntity> repository, ArmorPieceDto dto, ArmorSlot slot)
        where TEntity : ArmorPieceEntity, new()
    {
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == dto.Id);
        if (existing is null)
        {
            return AdminOperationResult<ArmorPieceDto>.NotFound("Armor piece not found");
        }

        var updated = ToEntity<TEntity>(dto);
        updated.Id = dto.Id;

        var result = await repository.UpdateAsync(updated);
        return AdminOperationResult<ArmorPieceDto>.FromSuccess(ToDto(result, slot));
    }

    private static async Task<AdminOperationResult<bool>> DeleteAsync<TEntity>(IBaseRepo<TEntity> repository, Guid id)
        where TEntity : ArmorPieceEntity
    {
        var existing = (await repository.GetAllAsync()).FirstOrDefault(e => e.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Armor piece not found");
        }

        await repository.DeleteAsync(existing);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
