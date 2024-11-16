// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.API.Controllers;
using BCD.Domain.Entities;
using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace BCD.Tests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ILogger<UsersController>> _loggerMock;
    private readonly Mock<IConfiguration> _configuration;
    private readonly UsersController _usersController;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _loggerMock = new Mock<ILogger<UsersController>>();
        _configuration = new Mock<IConfiguration>();
        _usersController = new UsersController(_userServiceMock.Object, _loggerMock.Object, _configuration.Object);
    }

    [Fact]
    public async Task GetAllUsers_ReturnOkResult_WithUserList()
    {
        // Arrange
        var users = new List<User>
        {
            new () { UserId = 1 , Username = "First User" , UserTypeId = 1, PasswordHash = "123", Email = "asd@ho.com", CreatedBy =1, CreatedAt = new DateTime(2024,1,13)},
            new () { UserId = 2 , Username = "Second User" , UserTypeId = 1, PasswordHash = "123", Email = "asd2@how.com", CreatedBy =1, CreatedAt = new DateTime(2024,1,13)},
        };

        _userServiceMock.Setup(x=>x.GetUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _usersController.GetAllUsers().ConfigureAwait(true);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var resultValue = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, resultValue.Count);
    }
}
