using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminWeaponService
{
    Task<AdminOperationResult<IReadOnlyCollection<WeaponDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<WeaponDto>> CreateAsync(string token, WeaponDto weaponDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<WeaponDto>> UpdateAsync(string token, WeaponDto weaponDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, Domain.Enums.WeaponTypeEntity type, CancellationToken cancellationToken = default);
}
