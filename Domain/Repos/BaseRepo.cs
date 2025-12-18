using Domain.Database;
using Domain.Entities.Items.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repos;

public abstract class BaseRepo<TEntity>(IGameDatabase database, Func<DatabaseModel, List<TEntity>> setAccessor)
    where TEntity : ItemsBaseEntity
{
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var databaseModel = await database.ReadAsync();
        setAccessor(databaseModel).Add(entity);
        await database.WriteAsync(databaseModel);

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var databaseModel = await database.ReadAsync();
        return setAccessor(databaseModel).ToList();
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var databaseModel = await database.ReadAsync();
        return setAccessor(databaseModel).AsQueryable().FirstOrDefault(predicate) ?? null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var databaseModel = await database.ReadAsync();
        return setAccessor(databaseModel).AsQueryable().Where(predicate).ToList();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var databaseModel = await database.ReadAsync();
        var set = setAccessor(databaseModel);
        var index = set.FindIndex(e => e.Id == entity.Id);
        if (index >= 0)
        {
            set[index] = entity;
        }
        else
        {
            set.Add(entity);
        }

        await database.WriteAsync(databaseModel);
        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        var databaseModel = await database.ReadAsync();
        var set = setAccessor(databaseModel);
        set.RemoveAll(e => e.Id == entity.Id);
        await database.WriteAsync(databaseModel);
    }
}
