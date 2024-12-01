// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Repositories;
using BCD.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace BCD.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly BCDDbContext _context;

    public UnitOfWork(BCDDbContext context, IConfiguration configuration)
    {
        _context = context;
        Users = new UserRepository(_context);
        Categories = new CategoryRepository(_context);
        Businesses = new BusinessRepository(_context, configuration);
        BusinessReviews = new BusinessReviewRepository(_context);
        Cities = new CityRepository(_context);
        // Initialize other repositories here
    }

    public IUserRepository Users { get; private set; }
    public ICategoryRepository Categories { get; private set; }
    public IBusinessRepository Businesses { get; private set; }
    public IBusinessReviewRepository BusinessReviews { get; private set; }
    public ICityRepository Cities { get; private set; }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

