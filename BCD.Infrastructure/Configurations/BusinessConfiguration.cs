// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCD.Infrastructure.Configurations;
public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.HasKey(x => x.BusinessId);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(x => x.Description)
            //.IsRequired()
            .HasMaxLength(2000);
        builder.Property(x => x.Latitude)
            .HasPrecision(9,6);
        builder.Property(x => x.Longitude)
            .HasPrecision(9,6);
    }
}
