using ApplicationServices.Contracts.Requests;
using ApplicationServices.Contracts.Responses;
using Domain.Entities.Storage;

namespace ApplicationServices.Services;

public interface IGameDataService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<MonsterProfile>> GetMonstersAsync(CancellationToken cancellationToken = default);

    Task<ProgressResponse?> GetProgressAsync(string token, CancellationToken cancellationToken = default);

    Task<bool> SaveProgressAsync(SaveProgressRequest request, CancellationToken cancellationToken = default);
}
