using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminUserService
{
    Task<AdminOperationResult<IReadOnlyCollection<AdminUserDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminUserDto>> CreateAsync(string token, AdminUserDto userDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminUserDto>> UpdateAsync(string token, Guid id, AdminUserDto userDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid id, CancellationToken cancellationToken = default);
}
