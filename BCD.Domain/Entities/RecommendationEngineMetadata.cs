// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BCD.Domain.Entities;
public class RecommendationEngineMetadata
{
    public int RecommendationEngineMetadataId { get; set; }
    public int UserId { get; set; }
    public int BusinessId { get; set; }
    public float RecommendationScore { get; set; }

    // Audit properties
    public DateTime GeneratedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}
