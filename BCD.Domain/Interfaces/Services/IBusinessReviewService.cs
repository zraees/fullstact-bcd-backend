// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.DTOs;
using BCD.Domain.Entities;

namespace BCD.Domain.Interfaces.Services;
public interface IBusinessReviewService
{
    Task<BusinessReview> AddReview(AddReviewDTO review);

    Task<IEnumerable<BusinessReview>> GetAllReviewsAsync();

    Task<IEnumerable<BusinessReview>> GetReviewsByBusinessIdAsync(int businessId);

    Task<IEnumerable<BusinessReview>> GetReviewsByUserIdAsync(int userId);
}
