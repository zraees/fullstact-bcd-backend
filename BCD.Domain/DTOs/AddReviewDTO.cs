// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BCD.Domain.DTOs;
public class AddReviewDTO
{
    public int businessId { get; set; }
    public int userId { get; set; }
    public int rating { get; set; }
    public string comment { get; set; }
}
