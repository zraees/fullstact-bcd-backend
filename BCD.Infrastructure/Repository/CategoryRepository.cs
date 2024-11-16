// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using BCD.Domain.Interfaces.Repositories;

namespace BCD.Infrastructure.Repository;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BCDDbContext context) : base(context) { }
}
