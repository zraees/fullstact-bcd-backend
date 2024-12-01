// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BCD.API.Dtos;

public class BusinessCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string HoursOfOperation { get; set; }
    public int CategoryId { get; set; }
    public bool IsFeatured { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int PostalCode { get; set; }
    public int CityID { get; set; }
    public List<IFormFile> Images { get; set; }
}
