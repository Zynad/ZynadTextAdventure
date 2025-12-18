using Domain.Database;
using Domain.Entities.Items.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repos;

public abstract class BaseRepo<TEntity> where TEntity : ItemsBaseEntity
{
    private readonly IGameDatabase _database;
    private readonly Func<DatabaseModel, List<TEntity>> _setAccessor;

    protected BaseRepo(IGameDatabase database, Func<DatabaseModel, List<TEntity>> setAccessor)
    {
        _database = database;
        _setAccessor = setAccessor;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var databaseModel = await _database.ReadAsync();
        _setAccessor(databaseModel).Add(entity);
        await _database.WriteAsync(databaseModel);

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var databaseModel = await _database.ReadAsync();
        return _setAccessor(databaseModel).ToList();
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var databaseModel = await _database.ReadAsync();
        return _setAccessor(databaseModel).AsQueryable().FirstOrDefault(predicate) ?? null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var databaseModel = await _database.ReadAsync();
        return _setAccessor(databaseModel).AsQueryable().Where(predicate).ToList();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var databaseModel = await _database.ReadAsync();
        var set = _setAccessor(databaseModel);
        var index = set.FindIndex(e => e.Id == entity.Id);
        if (index >= 0)
        {
            set[index] = entity;
        }
        else
        {
            set.Add(entity);
        }

        await _database.WriteAsync(databaseModel);
        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        var databaseModel = await _database.ReadAsync();
        var set = _setAccessor(databaseModel);
        set.RemoveAll(e => e.Id == entity.Id);
        await _database.WriteAsync(databaseModel);
    }
}
