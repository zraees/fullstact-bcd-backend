// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCD.Infrastructure.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.CategoryId);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(x => x.Description)
            //.IsRequired()
            .HasMaxLength(500);
    }
}
