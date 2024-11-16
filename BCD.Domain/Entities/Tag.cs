// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BCD.Domain.Entities;
public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Audit properties
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}
