// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BCD.Domain.Entities;

public class BusinessPhoto
{
    public int businessPhotoId { get; set; }
    public int BusinessId { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }

    // Audit properties
    public DateTime UploadedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }

    //public virtual Business Business { get; set; }
}
