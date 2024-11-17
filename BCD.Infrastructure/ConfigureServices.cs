// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Repositories;
using BCD.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCD.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BCDDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("BCDDbContext") ?? throw new InvalidOperationException("Connectionstring 'BCDDbContext' not found!"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,            // Maximum number of retry attempts
                        maxRetryDelay: TimeSpan.FromSeconds(30),  // Delay between retries
                        errorNumbersToAdd: null       // Optional: specify SQL error numbers to retry on
                    );
                });

            //opt.UseSqlite(builder.Configuration.GetConnectionString("BCDDbContext") ?? throw new InvalidOperationException("Connectionstring 'BCDDbContext' not found!"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        //services.AddAuthentication();

        //// register UserManager etc services related to Asp.Net Core Identity
        //var identityBuilder = services.AddIdentityCore<ApplicationUser>();
        //identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
        //identityBuilder.AddEntityFrameworkStores<UserIdentityDbContext>().AddDefaultTokenProviders();
        //identityBuilder.AddSignInManager<SignInManager<ApplicationUser>>();

        return services;
    }


}
