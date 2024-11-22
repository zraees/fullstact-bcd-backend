// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.DTOs;
using BCD.Domain.Entities;

namespace BCD.Domain.Interfaces.Services;
public interface IBusinessService
{
    Task<BusinessReview> AddReview(AddReviewDTO review);

    Task<IEnumerable<Business>> GetBusinessesAsync();
    //Task<int> CreateUserAsync(Business Business);

    Task<IEnumerable<Business>> GetFeatureBusinessesAsync();
}
