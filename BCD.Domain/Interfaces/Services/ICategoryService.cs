// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;

namespace BCD.Domain.Interfaces.Services;
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    //Task<int> CreateUserAsync(Category Category);
}
