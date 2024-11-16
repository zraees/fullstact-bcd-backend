// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using BCD.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BCD.Infrastructure.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    protected readonly BCDDbContext _context;
    protected readonly DbSet<User> _dbSet;

    public UserRepository(BCDDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<User>();
    }

    public async Task<User> IsAuthenticated(string email, string pwd)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == pwd).ConfigureAwait(false);
    }
}
