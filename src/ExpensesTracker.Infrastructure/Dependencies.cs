﻿using ExpensesTracker.Domain.Infrastructure.Hasher;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using ExpensesTracker.Domain.Repositories;
using ExpensesTracker.Domain.Repositories.Category;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Infrastructure.Authentication;
using ExpensesTracker.Infrastructure.Data;
using ExpensesTracker.Infrastructure.Hasher;
using ExpensesTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesTracker.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddTokenServices();
        services.AddHasher();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => {
            var connectionString = configuration.GetConnectionString("MySql");
            var version = ServerVersion.AutoDetect(connectionString);

            options.UseMySql(connectionString, version);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();

        services.AddScoped<ICategoryReadRepository, CategoryRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryRepository>();

        services.AddScoped<ExpenseRepository>();
        services.AddScoped<ExpenseRepository>();
    }

    private static void AddTokenServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IJwtValidator, JwtValidator>();
    }

    private static void AddHasher(this IServiceCollection services)
    {
        services.AddScoped<IHasherService, BCryptHasher>();
    }
}