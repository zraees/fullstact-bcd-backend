// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using BCD.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BCD.Infrastructure;
public class BCDDbContext : DbContext
{
    public BCDDbContext(DbContextOptions<BCDDbContext> dbContextOptions) : base(dbContextOptions)
    {
        // Ensure database is migrated to the latest version
        Database.Migrate();
    }

    public DbSet<UserType> UserTypes { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Business> Businesses { get; set; }

    public DbSet<BusinessPhoto> BusinessPhotos { get; set; }

    public DbSet<BusinessReview> BusinessReviews { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntities = ChangeTracker.Entries()
                                            .Where(x => x.State == EntityState.Added
                                                    || x.State == EntityState.Modified
                                                    || x.State == EntityState.Deleted)
                                            .ToList();

        //foreach (var entity in modifiedEntities)
        //{
        //    var auditLog = new AuditLog
        //    {
        //        EntityName = entity.Entity.GetType().Name,
        //        Email = "test",
        //        Action = entity.State.ToString(),
        //        Timestamp = DateTime.UtcNow,
        //        Changes = GetModifiedChanges(entity)
        //    };

        //    AuditLogs.Add(auditLog);
        //}

        return base.SaveChangesAsync(cancellationToken);
    }

    //private string GetModifiedChanges(EntityEntry entity)
    //{
    //    var changes = new StringBuilder();

    //    foreach (var property in entity.OriginalValues.Properties)
    //    {
    //        var originalVal = entity.OriginalValues[property];
    //        var currentVal = entity.CurrentValues[property];

    //        if (Equals(originalVal, currentVal) == false)
    //        {
    //            changes.AppendLine($"{property.Name}: Original '{originalVal}' Changed to '{currentVal}'");
    //        }
    //    }

    //    return changes.ToString();
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new BusinessConfiguration());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserType>().HasData(
            new UserType { UserTypeId = 1, UserTypeName = "admin", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 0 },
            new UserType { UserTypeId = 2, UserTypeName = "User", CreatedAt = new DateTime(2024, 10, 11), CreatedBy = 0 }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Username = "admin", Email = "developerxone@hotmail.com", PasswordHash = "admin123", UserTypeId = 1, CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 0 }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Food", Description = "", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 1 },
            new Category { CategoryId = 2, Name = "Coffee", Description = "", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 1 },
            new Category { CategoryId = 3, Name = "Nightlife", Description = "", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 1 },
            new Category { CategoryId = 4, Name = "Fun", Description = "", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 1 },
            new Category { CategoryId = 5, Name = "Shopping", Description = "", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 1 },
            new Category { CategoryId = 6, Name = "Grocery", Description = "", CreatedAt = new DateTime(2024, 10, 1), CreatedBy = 1 },
            new Category { CategoryId = 7, Name = "Gym", Description = "", CreatedAt = new DateTime(2024, 10, 11), CreatedBy = 1 }
        );

        //modelBuilder.Entity<Business>() .HasData(
        //    new Business() { BusinessId = 1, Name = "", CategoryId =1, }
    }
}
