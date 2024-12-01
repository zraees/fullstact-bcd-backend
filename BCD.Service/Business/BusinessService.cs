// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace BCD.Service.Business;
public class BusinessService : IBusinessService
{
    private readonly IUnitOfWork _unitOfWork;

    public BusinessService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Business> AddAsync(Domain.Entities.Business business)
    {
        await _unitOfWork.Businesses.AddAsync(business).ConfigureAwait(false);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        return business;
    }

    public async Task DeleteAsync(int businessId)
    {
        await _unitOfWork.Businesses.DeleteAsync(businessId).ConfigureAwait(false);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Domain.Entities.Business>> GetBusinessesAsync()
    {
        return await _unitOfWork.Businesses.GetAllAsync("BusinessPhotos", "Category", "City", "BusinessReviews.User").ConfigureAwait(false);
    }

    public async Task<IEnumerable<Domain.Entities.Business>> GetFeatureBusinessesAsync()
    {
        return await _unitOfWork.Businesses.GetAsync(x => x.IsFeatured, "BusinessPhotos", "Category", "City", "BusinessReviews.User").ConfigureAwait(false);
    }

    public async Task<Domain.Entities.Business> MarkAsFeatured(int businessId)
    {
        var business = await _unitOfWork.Businesses.GetByIdAsync(businessId).ConfigureAwait(false);

        if (business == null)
        {
            throw new Exception("Business not found");
        }

        business.IsFeatured = !business.IsFeatured;

        await _unitOfWork.Businesses.UpdateAsync(business).ConfigureAwait(false);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        return business;
    }
}
