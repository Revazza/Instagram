using Instagram.Application.Authentication;
using Instagram.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Instagram.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }

}