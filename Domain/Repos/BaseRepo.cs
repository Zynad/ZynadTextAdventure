﻿using Domain.Contexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        if (entity != null)
            return entity;

        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();

        if (entities != null)
            return entities;

        return null!;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}

