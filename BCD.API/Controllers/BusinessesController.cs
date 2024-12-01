// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.API.Dtos;
using BCD.Domain.Entities;
using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BCD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BusinessesController : ControllerBase
{
    private readonly IBusinessService _businessService;
    private readonly ILogger<BusinessesController> _logger;
    private readonly IWebHostEnvironment _environment;

    public BusinessesController(IBusinessService BusinessService, ILogger<BusinessesController> logger, IWebHostEnvironment environment)
    {
        _businessService = BusinessService;
        _logger = logger;
        _environment = environment;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> CreateBusiness([FromForm] BusinessCreateDto businessDto)
    {
        // Save Business data
        var business = new Business
        {
            Name = businessDto.Name,
            Description = businessDto.Description,
            Address = businessDto.Address,
            PhoneNumber = businessDto.PhoneNumber,
            Email = businessDto.Email,
            Website = businessDto.Website,
            HoursOfOperation = businessDto.HoursOfOperation,
            CategoryId = businessDto.CategoryId,
            OwnerId = 1,//businessDto.OwnerId,
            IsFeatured = businessDto.IsFeatured,
            Latitude = businessDto.Latitude,
            Longitude = businessDto.Longitude,
            PostalCode = businessDto.PostalCode,
            CityID = businessDto.CityID
        };

        // Save Images
        var businessPhotos = new List<BusinessPhoto>();
        if (businessDto.Images != null && businessDto.Images.Count > 0)
        {
            foreach (var image in businessDto.Images)
            {
                var businessPhoto = new BusinessPhoto();
                businessPhoto.FileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                businessPhoto.stream = image.OpenReadStream();
                businessPhoto.ContentType = image.ContentType;

                businessPhoto.Description = image.FileName;
                businessPhoto.UpdatedAt = DateTime.Now;
                businessPhoto.CreatedAt = DateTime.Now;
                businessPhoto.CreatedBy = 1;
                businessPhoto.UpdatedBy = 1;

                businessPhotos.Add(businessPhoto);
            }
        }

        business.BusinessPhotos = businessPhotos;
        await _businessService.AddAsync(business).ConfigureAwait(false);

        //await transaction.CommitAsync();
        return Ok(new { message = "Business created successfully" });
    }

    [HttpGet("GetBusinesses/{categoryId}/{cityId}/{searchText}")]
    public async Task<IActionResult> GetBusinesses(int categoryId, int cityId, string searchText)
    {
        // get all Users through injected-service
        var data = await _businessService.GetBusinessesAsync(categoryId, cityId, searchText).ConfigureAwait(false);

        var result = data.Select(x => new
        {
            x.BusinessId,
            x.Name,
            x.Description,
            x.Website,
            x.Address,
            x.Email,
            x.HoursOfOperation,
            x.PhoneNumber,
            x.IsFeatured,
            x.Latitude,
            x.Longitude,
            x.PostalCode,
            City = new { x.CityID, x.City?.CityName },
            Category = new
            {
                x.Category?.CategoryId,
                x.Category?.Name
            },
            BusinessPhotos = x.BusinessPhotos?.Select(p => new
            {
                p.businessPhotoId,
                p.BusinessId,
                p.Url
            }),
            BusinessReviews = new List<BusinessReview>()
            //x.BusinessReviews?.Select(p => new
            //{
            //    p.BusinessReviewId,
            //    p.BusinessId,
            //    p.Rating,
            //    p.Comment,
            //    p.CreatedAt,
            //    User = new { p.UserId, p.User.Username }
            //}),
        });

        return Ok(result);
    }

    /// <summary>
    /// first endpoint: to bring all Businesses data from csv and return to client in json format
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllBusinesses")]
    public async Task<IActionResult> GetAllBusinesses()
    {
        // get all Users through injected-service
        var data = await _businessService.GetBusinessesAsync().ConfigureAwait(false);

        var result = data.Select(x => new
        {
            x.BusinessId,
            x.Name,
            x.Description,
            x.Website,
            x.Address,
            x.Email,
            x.HoursOfOperation,
            x.PhoneNumber,
            x.IsFeatured,
            x.Latitude,
            x.Longitude,
            x.PostalCode,
            City = new { x.CityID, x.City?.CityName },
            Category = new
            {
                x.Category?.CategoryId,
                x.Category?.Name
            },
            BusinessPhotos = x.BusinessPhotos?.Select(p => new
            {
                p.businessPhotoId,
                p.BusinessId,
                p.Url
            }),
            BusinessReviews = new List<BusinessReview>()
            //x.BusinessReviews?.Select(p => new
            //{
            //    p.BusinessReviewId,
            //    p.BusinessId,
            //    p.Rating,
            //    p.Comment,
            //    p.CreatedAt,
            //    User = new { p.UserId, p.User.Username }
            //}),
        });

        return Ok(result);
    }

    [HttpGet("GetFeatureBusinesses")]
    public async Task<IActionResult> GetFeatureBusinesses()
    {
        // get all Users through injected-service
        var data = await _businessService.GetFeatureBusinessesAsync().ConfigureAwait(false);

        var result = data.Select(x => new
        {
            x.BusinessId,
            x.Name,
            x.Description,
            x.Website,
            x.Address,
            x.Email,
            x.HoursOfOperation,
            x.PhoneNumber,
            x.IsFeatured,
            x.Latitude,
            x.Longitude,
            x.PostalCode,
            City = new { x.CityID, x.City?.CityName },
            Category = new
            {
                x.Category?.CategoryId,
                x.Category?.Name
            },
            BusinessPhotos = x.BusinessPhotos?.Select(p => new
            {
                p.businessPhotoId,
                p.BusinessId,
                p.Url
            }),
            BusinessReviews = new List<BusinessReview>()
            //x.BusinessReviews?.Select(p => new
            //{
            //    p.BusinessReviewId,
            //    p.BusinessId,
            //    p.Rating,
            //    p.Comment,
            //    p.CreatedAt,
            //    User = new { p.UserId }
            //})
        });

        return Ok(result);
    }

    [HttpPut("MarkAsFeatured/{businessId}")]
    public async Task<IActionResult> MarkAsFeatured(int businessId)
    {
        var business = await _businessService.MarkAsFeatured(businessId).ConfigureAwait(false);
        return Ok(business);
    }

    [HttpDelete("Delete/{businessId}")]
    public async Task<IActionResult> Delete(int businessId)
    {
        await _businessService.DeleteAsync(businessId).ConfigureAwait(false);
        return Ok();
    }
}
