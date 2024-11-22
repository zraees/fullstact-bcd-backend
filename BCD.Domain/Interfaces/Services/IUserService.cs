// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using Microsoft.Win32;

namespace BCD.Domain.Interfaces.Services;
public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User> IsAuthenticated(string email, string pwd);

    Task<User> Register(string username, string email, string pwd);
    
    Task<User> GetUserByIdAsync(int userId);

    //Task<int> CreateUserAsync(User User);
}
