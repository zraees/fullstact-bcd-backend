// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces.Repositories;

namespace BCD.Domain.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    IBusinessRepository Businesses { get; }
    IBusinessReviewRepository BusinessReviews { get; }
    ICityRepository Cities { get; }
    // Add other repositories here

    Task<int> SaveAsync();
}
