// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net.Http.Json;
using BCD.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BCD.IntegrationTests;

public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Tests GetAllUsers endpoint, checking Ok-response, and get user list as expected.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetAllUsers_ReturnsOk_WhenUsersExist()
    {
        // Arrange
        // if we have something to arrange for our test case

        // Act
        var response = await _client.GetAsync("/api/Users/GetAllUsers").ConfigureAwait(true);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var users = await response.Content.ReadFromJsonAsync<List<User>>().ConfigureAwait(true);
        users.Should().NotBeNull();       // used FluentAssertions for more readable assertions
        users.Should().NotBeEmpty();
    }

}
