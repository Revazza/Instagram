using FluentValidation;
using Instagram.Application.Authentication;
using Instagram.Application.Common;
using Instagram.Application.Common.Behaviour;
using Instagram.Application.Common.Extensions.BuiltInTypes;
using Instagram.Application.Services;
using Instagram.Domain.Users;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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


        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMappingsConfigurations();
        services.AddAuthenticationConfigurations(configuration);

        return services;
    }

    private static IServiceCollection AddMappingsConfigurations(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();

        return services;
    }

    private static void AddAuthenticationConfigurations(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var issuer = configuration[$"{JwtSettings.SectionName}:Issuer"]!;
        var audience = configuration[$"{JwtSettings.SectionName}:Audience"]!;
        var secretKey = configuration[$"{JwtSettings.SectionName}:SecretKey"]!;
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });

    }


}