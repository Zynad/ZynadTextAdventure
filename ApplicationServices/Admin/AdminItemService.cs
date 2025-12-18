using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using Domain.Entities.Items.Models;
using Domain.Repos.Items;
using System.Linq;

namespace ApplicationServices.Admin;

public class AdminItemService(GetCurrentUserHandler getCurrentUserHandler, IItemRepository itemRepository)
    : IAdminItemService
{
    public async Task<AdminOperationResult<IReadOnlyCollection<ItemDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<ItemDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var items = await itemRepository.GetAllAsync();
        return AdminOperationResult<IReadOnlyCollection<ItemDto>>.FromSuccess(items.Select(ToDto).ToList());
    }

    public async Task<AdminOperationResult<ItemDto>> CreateAsync(string token, ItemDto itemDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<ItemDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(itemDto.Name))
        {
            return AdminOperationResult<ItemDto>.ValidationFailed("Name is required");
        }

        var entity = ToEntity(itemDto);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        var persisted = await itemRepository.AddAsync(entity);
        return AdminOperationResult<ItemDto>.FromSuccess(ToDto(persisted));
    }

    public async Task<AdminOperationResult<ItemDto>> UpdateAsync(string token, ItemDto itemDto, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<ItemDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var existing = (await itemRepository.GetAllAsync()).FirstOrDefault(i => i.Id == itemDto.Id);
        if (existing is null)
        {
            return AdminOperationResult<ItemDto>.NotFound("Item not found");
        }

        var entity = ToEntity(itemDto);
        entity.Id = itemDto.Id;
        var persisted = await itemRepository.UpdateAsync(entity);
        return AdminOperationResult<ItemDto>.FromSuccess(ToDto(persisted));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var existing = (await itemRepository.GetAllAsync()).FirstOrDefault(i => i.Id == id);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Item not found");
        }

        await itemRepository.DeleteAsync(existing);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static ItemDto ToDto(GenericItemEntity entity)
    {
        return new ItemDto(
            entity.Id,
            entity.Name,
            entity.LevelRequirement,
            entity.Rarity,
            entity.Value,
            entity.Weight);
    }

    private static GenericItemEntity ToEntity(ItemDto dto)
    {
        return new GenericItemEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            LevelRequirement = dto.LevelRequirement,
            Rarity = dto.Rarity,
            Value = dto.Value,
            Weight = dto.Weight
        };
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
