using Instagram.Infrastructure.Db;
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
        return services;
    }
}