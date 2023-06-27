using Instagram.Application.Interfaces;
using Instagram.Infrastructure.Db;
using Instagram.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            op.UseSqlServer(configure.GetConnectionString(InstagramDbContext.ConnectionString));
        });
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

}

