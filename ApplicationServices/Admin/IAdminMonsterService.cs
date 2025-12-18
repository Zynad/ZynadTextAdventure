using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminMonsterService
{
    Task<AdminOperationResult<IReadOnlyCollection<MonsterDto>>> GetAllAsync(string token, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<MonsterDto>> CreateAsync(string token, MonsterDto monsterDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<MonsterDto>> UpdateAsync(string token, string name, MonsterDto monsterDto, CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteAsync(string token, string name, CancellationToken cancellationToken = default);
}
