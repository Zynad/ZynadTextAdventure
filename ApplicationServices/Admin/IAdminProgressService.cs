using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminProgressService
{
    Task<AdminOperationResult<IReadOnlyCollection<AdminProgressDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminProgressDto>> CreateAsync(string token, AdminProgressDto progressDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminProgressDto>> UpdateAsync(string token, Guid userId, AdminProgressDto progressDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(string token, Guid userId, CancellationToken cancellationToken = default);
}
