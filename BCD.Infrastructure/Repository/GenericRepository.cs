// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq.Expressions;
using BCD.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BCD.Infrastructure.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly BCDDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(BCDDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    // Overload to accept navigation properties
    public async Task<IEnumerable<T>> GetAllAsync(params string[] navigationProperties)
    {
        IQueryable<T> query = _dbSet;

        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty);
        }

        return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
    {
        //return await _dbSet.Where(predicate).ToListAsync();
        IQueryable<T> query = _dbSet.Where(predicate);

        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty);
        }

        return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
    }

    //public async Task<T> GetByIdAsync(int id)
    //{ 
    //    return await _dbSet.AsNoTracking().FindAsync(id);
    //}
    public async Task<T> GetByIdAsync(int id)
    {
        var keyProperty = _dbSet.EntityType.FindPrimaryKey().Properties.First().Name;
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty) == id).ConfigureAwait(false);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
}

