// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces.Services;
using BCD.Service.Category;
using Microsoft.AspNetCore.Mvc;

namespace BCD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _CategoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService CategoryService, ILogger<CategoriesController> logger)
    {
        _CategoryService = CategoryService;
        _logger = logger;
    }

    /// <summary>
    /// first endpoint: to bring all Categories data from csv and return to client in json format
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        // get all Users through injected-service
        var data = await _CategoryService.GetCategoriesAsync().ConfigureAwait(false);
        return Ok(data);

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

}
