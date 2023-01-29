
using System.Security.Claims;
using System.Text;
using Logins.Api.Configurations;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Logins.Api;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddRabbitMqEventBus(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x => {
            var eventBusConfiguration = config?.GetSection("RabbitMQ")?.Get<RabbitMqConfiguration>();

            if (eventBusConfiguration != null && eventBusConfiguration.IsValid)
            {
                x.UsingRabbitMq((context, busConfig) => {
                    busConfig.Host(eventBusConfiguration.Host, eventBusConfiguration.Port, eventBusConfiguration.VirtualHost, c => {
                        c.Username(eventBusConfiguration.Username);
                        c.Password(eventBusConfiguration.Password);
                    });
                });
            }
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtConfiguration = config.GetSection("Jwt").Get<JwtConfiguration>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = jwtConfiguration.Audience,
                    ValidIssuer = jwtConfiguration.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
                };
            });

        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
        });

        return services;
    }
}