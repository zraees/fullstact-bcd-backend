// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Services;

namespace BCD.Service.City;
public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Domain.Entities.City>> GetCitiesAsync()
    {
        return await _unitOfWork.Cities.GetAllAsync().ConfigureAwait(false);
    }
}
