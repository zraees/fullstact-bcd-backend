// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.DTOs;
using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BusinessReviewsController : ControllerBase
{
    private readonly IBusinessReviewService _businessReviewService;
    private readonly ILogger<BusinessReviewsController> _logger;

    public BusinessReviewsController(IBusinessReviewService businessReviewService, ILogger<BusinessReviewsController> logger)
    {
        _businessReviewService = businessReviewService;
        _logger = logger;
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview(AddReviewDTO addReview)
    {
        var reivew = await _businessReviewService.AddReview(addReview).ConfigureAwait(false);
        if (reivew == null)
        {
            return BadRequest("Error while adding review!");
        }
        else
        {
            return Ok(reivew);
        }
    }

    [HttpGet("GetBusinessReviews/{businessId}")]
    public async Task<IActionResult> GetBusinessReviews(int businessId)
    {
        // get all Users through injected-service
        var data = await _businessReviewService.GetReviewsByBusinessIdAsync(businessId).ConfigureAwait(false);

        var result = data.OrderByDescending(x => x.CreatedAt).Select(p => new
        {
            p.BusinessReviewId,
            p.BusinessId,
            p.Rating,
            p.Comment,
            p.CreatedAt,
            User = new { p.UserId, p.User.Username }
        });

        return Ok(result);
    }


    [HttpGet("GetReviewsByUserIdAsync/{userId}")]
    public async Task<IActionResult> GetReviewsByUserIdAsync(int userId)
    {
        // get all Users through injected-service
        var data = await _businessReviewService.GetReviewsByUserIdAsync(userId).ConfigureAwait(false);

        var result = data.OrderByDescending(x => x.CreatedAt).Select(p => new
        {
            p.BusinessReviewId,
            p.BusinessId,
            p.Rating,
            p.Comment,
            p.CreatedAt,
            Business = new { p.Business.Name, p.Business.Address },
            User = new { p.UserId, p.User.Username }
        });

        return Ok(result);
    }
    
}
