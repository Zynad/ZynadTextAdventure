namespace Domain.Database;

public interface IGameDatabase
{
    Task<DatabaseModel> ReadAsync(CancellationToken cancellationToken = default);

    Task WriteAsync(DatabaseModel databaseModel, CancellationToken cancellationToken = default);
}
