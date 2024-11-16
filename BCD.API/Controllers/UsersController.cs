// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCD.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate(string email, string pwd)
    {
        var user =await _userService.IsAuthenticated(email, pwd).ConfigureAwait(false);
        if (user == null)
        {
            return BadRequest("Invalid user and password!");
        }
        else
        {
            return Ok(user);
        }
    }

    [HttpGet("GetUser")]
    public IActionResult GetUser()
    {
        return Ok("thank you very much ..: " + DateTime.Now.ToShortTimeString());
    }

    /// <summary>
    /// first endpoint: to bring all Users data from csv and return to client in json format
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        // get all Users through injected-service
        var Users = await _userService.GetUsersAsync().ConfigureAwait(false);
        return Ok(Users);

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
