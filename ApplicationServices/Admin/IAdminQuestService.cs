using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminQuestService
{
    Task<AdminOperationResult<IReadOnlyCollection<AdminQuestDto>>> GetAllAsync(
        string token,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminQuestDto>> CreateAsync(
        string token,
        AdminQuestDto questDto,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminQuestDto>> UpdateAsync(
        string token,
        string questId,
        AdminQuestDto questDto,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(
        string token,
        string questId,
        CancellationToken cancellationToken = default);
}
