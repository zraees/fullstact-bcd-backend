// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;

namespace BCD.Domain.Interfaces.Services;
public interface IBusinessService
{
    Task<Business> AddAsync(Business business);

    Task<Business> MarkAsFeatured(int businessId);

    Task DeleteAsync(int businessId);

    Task<IEnumerable<Business>> GetBusinessesAsync();

    Task<IEnumerable<Business>> GetFeatureBusinessesAsync();
}
