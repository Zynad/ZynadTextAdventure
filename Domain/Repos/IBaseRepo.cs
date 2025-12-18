using Domain.Entities.Items.Models;
using System.Linq.Expressions;

namespace Domain.Repos;

public interface IBaseRepo<TEntity> where TEntity : ItemsBaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
