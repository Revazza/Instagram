using Instagram.Application.Interfaces;
using Instagram.Domain.Users;
using Instagram.Infrastructure.Db;
using Instagram.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Instagram.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configure
        )
    {
        services.AddDbContext<InstagramDbContext>(op =>
        {
            op.UseSqlServer(configure.GetConnectionString(InstagramDbContext.ConnectionString))
            .LogTo(s => Debug.WriteLine(s))
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true);
        });
        services
            .AddRepositories()
            .AddIdentity();
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<UserId>>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 2;
            o.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<InstagramDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IStoryRepository, StoryRepository>();

        return services;
    }

}

