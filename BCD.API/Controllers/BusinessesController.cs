// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.DTOs;
using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BusinessesController : ControllerBase
{
    private readonly IBusinessService _businessService;
    private readonly ILogger<BusinessesController> _logger;

    public BusinessesController(IBusinessService BusinessService, ILogger<BusinessesController> logger)
    {
        _businessService = BusinessService;
        _logger = logger;
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview(AddReviewDTO addReview)
    {
        var reivew = await _businessService.AddReview(addReview).ConfigureAwait(false);
        if (reivew == null)
        {
            return BadRequest("Error while adding review!");
        }
        else
        {
            return Ok(reivew);
        }
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
            BusinessReviews = x.BusinessReviews?.Select(p => new
            {
                p.BusinessReviewId,
                p.BusinessId,
                p.Rating,
                p.Comment,
                p.CreatedAt,
                User = new { p.UserId, p.User.Username }
            }),
        });

        return Ok(result);

        //// if return with sucess, show User data to client other wise badreqeust with error message.
        //if (Users.IsSuccess)
        //{
        //    return Ok(Users.Value);
        //}
        //else
        //{
        //    _logger.LogError(Users.Error);
        //    return BadRequest(Users.Error);
        //}
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
            BusinessReviews = x.BusinessReviews?.Select(p => new
            {
                p.BusinessReviewId,
                p.BusinessId,
                p.Rating,
                p.Comment,
                p.CreatedAt,
                User = new { p.UserId }
            })
        });

        return Ok(result);
    }
}
