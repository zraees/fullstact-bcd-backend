// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.DTOs;
using BCD.Domain.Entities;
using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Services;

namespace BCD.Service.Business;
public class BusinessReviewService : IBusinessReviewService
{
    private readonly IUnitOfWork _unitOfWork;

    public BusinessReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BusinessReview> AddReview(AddReviewDTO review)
    {
        //var business = await _unitOfWork.Businesses.GetByIdAsync(review.businessId).ConfigureAwait(false);
        //if (business == null) {
        //    return null;
        //}

        var businessReview = new BusinessReview()
        {
            BusinessId = review.businessId,
            UserId = review.userId,
            Rating = review.rating,
            Comment = review.comment,
            CreatedAt = DateTime.Now,
            CreatedBy = review.userId,
            User = null,
        };

        await _unitOfWork.BusinessReviews.AddAsync(businessReview).ConfigureAwait(false);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        return businessReview;
    }

    public async Task<IEnumerable<BusinessReview>> GetReviewsByBusinessIdAsync(int businessId)
    {
        return await _unitOfWork.BusinessReviews.GetAsync(x => x.BusinessId == businessId, "User").ConfigureAwait(false);
    }

}
