using Domain.Contexts;
using System.Linq.Expressions;

namespace Domain.Repos;

public abstract class BaseRepo<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    public BaseRepo(DataContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return _context.Set<TEntity>().ToList() ?? [];
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().FirstOrDefault(predicate) ?? null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Where(predicate).ToList() ?? [];
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
        return entity;
    }
    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }
}

