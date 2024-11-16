// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;

namespace BCD.Domain.Interfaces.Services;
public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User> IsAuthenticated(string email, string pwd);

    Task<User> GetUserByIdAsync(int userId);

    //Task<int> CreateUserAsync(User User);
}
