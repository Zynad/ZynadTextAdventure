using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminItemService
{
    Task<AdminOperationResult<IReadOnlyCollection<ItemDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<ItemDto>> CreateAsync(string token, ItemDto itemDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<ItemDto>> UpdateAsync(string token, ItemDto itemDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, CancellationToken cancellationToken = default);
}
