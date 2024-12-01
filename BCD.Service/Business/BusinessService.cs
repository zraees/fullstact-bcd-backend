// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<IEnumerable<Domain.Entities.Business>> GetBusinessesAsync(int categoryId, int cityId, string searchText)
    {
        searchText = searchText.Replace("-1", "").Trim();

        return await _unitOfWork.Businesses.GetAsync(x => (cityId > 0 ? x.CityID == cityId : true)
                                                    && (categoryId > 0 ? x.CategoryId == categoryId : true)
                                                    && x.Description.Contains(searchText),
                                                "BusinessPhotos", "Category", "City", "BusinessReviews.User").ConfigureAwait(false);
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

    public async Task<List<Domain.Entities.Business>> GetRecommendations(int userId)
    {
        // Fetch up to 3 recommended shops based on user remarks
        var recommendedBusinesses = _unitOfWork.BusinessReviews.GetAsync(r => r.UserId == userId).Result
            .Select(r => r.BusinessId)
            .Take(3) // Limit to 3 shops
            .ToList();

        var businessRecommendations = _unitOfWork.Businesses.GetAsync(s => recommendedBusinesses.Contains(s.BusinessId),
                                                                "BusinessPhotos", "Category", "City", "BusinessReviews.User").Result
            .Take(3)
            .ToList();

        // Calculate how many slots are left for featured shops
        int remainingSlots = 3 - (businessRecommendations?.Count ?? 0);

        if (remainingSlots > 0)
        {
            // Fetch featured shops to fill remaining slots
            var featuredShops = _unitOfWork.Businesses.GetAsync(s => s.IsFeatured && !recommendedBusinesses.Contains(s.BusinessId),
                                                            "BusinessPhotos", "Category", "City", "BusinessReviews.User").Result
                .Take(remainingSlots)
                .ToList();

            businessRecommendations.AddRange(featuredShops);
        }

        return businessRecommendations;
    }
}
