using ApplicationServices.Admin.Models;

namespace ApplicationServices.Admin;

public interface IAdminWorldService
{
    Task<AdminOperationResult<IReadOnlyCollection<TownDto>>> GetTownsAsync(
        string token,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<TownDto>> CreateTownAsync(
        string token,
        TownDto townDto,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<TownDto>> UpdateTownAsync(
        string token,
        string townName,
        TownDto townDto,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteTownAsync(
        string token,
        string townName,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<TownNpcDto>> CreateNpcAsync(
        string token,
        string townName,
        TownNpcDto townNpc,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<TownNpcDto>> UpdateNpcAsync(
        string token,
        string townName,
        string npcId,
        TownNpcDto townNpc,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteNpcAsync(
        string token,
        string townName,
        string npcId,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<IReadOnlyCollection<AdminWorldLocationDto>>> GetLocationsAsync(
        string token,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminWorldLocationDto>> CreateLocationAsync(
        string token,
        AdminWorldLocationDto location,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<AdminWorldLocationDto>> UpdateLocationAsync(
        string token,
        string locationId,
        AdminWorldLocationDto location,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteLocationAsync(
        string token,
        string locationId,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<IReadOnlyCollection<DropTableDto>>> GetDropTablesAsync(
        string token,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<DropTableDto>> UpsertDropTableAsync(
        string token,
        DropTableDto dropTable,
        CancellationToken cancellationToken = default);

    Task<AdminOperationResult<bool>> DeleteDropTableAsync(
        string token,
        string biome,
        CancellationToken cancellationToken = default);
}
