// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace BCD.Domain.Entities;
public class Business
{
    public int BusinessId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string HoursOfOperation { get; set; } // JSON as string
    public int CategoryId { get; set; }
    public int OwnerId { get; set; }
    public bool IsFeatured { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int PostalCode { get; set; }
    public int CityID { get; set; }

    // Audit properties
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }

    public virtual User Owner { get; set; }
    public virtual Category Category { get; set; }
    public virtual City City { get; set; }

    public virtual ICollection<BusinessPhoto> BusinessPhotos { get; set; } = new List<BusinessPhoto>();

    public virtual ICollection<BusinessReview> BusinessReviews { get; set; } = new List<BusinessReview>();
}
