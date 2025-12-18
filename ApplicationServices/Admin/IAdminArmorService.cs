using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminArmorService
{
    Task<AdminOperationResult<IReadOnlyCollection<ArmorPieceDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<ArmorPieceDto>> CreateAsync(string token, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<ArmorPieceDto>> UpdateAsync(string token, ArmorPieceDto armorPieceDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, ArmorSlot slot, CancellationToken cancellationToken = default);
}
