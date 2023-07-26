using FluentValidation;
using Instagram.Application.Authentication;
using Instagram.Application.Common.Behaviour;
using Instagram.Application.Hubs;
using Instagram.Application.Hubs.Chat;
using Instagram.Application.Hubs.Notification;
using Instagram.Application.Services;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
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
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //Custom methods
        services
            .AddSignalRWithConnections()
            .AddMappingsConfigurations()
            .AddAuthenticationConfigurations(configuration)
            .AddCorsPolicy();

        return services;
    }

    private static IServiceCollection AddSignalRWithConnections(
        this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<IChatHubConnections, ChatHubConnections>();
        services.AddSingleton<IUserConnections, UserConnections>();

        return services;
    }

    private static IServiceCollection AddCorsPolicy(
        this IServiceCollection services)
    {

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

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

    private static IServiceCollection AddAuthenticationConfigurations(
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
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/ChatHub") ||
                            (path.StartsWithSegments("/NotificationHub") )
                            ))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = tokenValidationParameters;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("User",
                policy => policy.RequireClaim(ClaimTypes.Role, "user"));

        });

        return services;
    }


}