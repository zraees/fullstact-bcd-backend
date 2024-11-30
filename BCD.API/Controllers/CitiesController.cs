// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly ICityService _CityService;
    private readonly ILogger<CitiesController> _logger;

    public CitiesController(ICityService cityService, ILogger<CitiesController> logger)
    {
        _CityService = cityService;
        _logger = logger;
    }

    /// <summary>
    /// first endpoint: to bring all Cities data from csv and return to client in json format
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCities")]
    public async Task<IActionResult> GetAllCities()
    {
        // get all Users through injected-service
        var data = await _CityService.GetCitiesAsync().ConfigureAwait(false);
        return Ok(data);
    }
}
