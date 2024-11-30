// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;

namespace BCD.Domain.Interfaces.Services;
public interface ICityService
{
    Task<IEnumerable<City>> GetCitiesAsync(); 
}
