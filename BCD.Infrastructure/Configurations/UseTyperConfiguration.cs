// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCD.Infrastructure.Configurations;
public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
{
    public void Configure(EntityTypeBuilder<UserType> builder)
    {
        builder.HasKey(x => x.UserTypeId);
        builder.Property(x => x.UserTypeName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
