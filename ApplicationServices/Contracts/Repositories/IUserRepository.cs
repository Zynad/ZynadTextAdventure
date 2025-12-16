using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Repositories;

public interface IUserRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Account?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Account?> GetBySessionTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Account>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
}
