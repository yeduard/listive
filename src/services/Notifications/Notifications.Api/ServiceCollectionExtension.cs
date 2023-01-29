using System.Security.Claims;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Notifications.Api.Options;
using Notifications.Api.Consumers;

namespace Notifications.Api;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddRabbitMqEventBus(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            var eventBusConfiguration = config?.GetSection("RabbitMQ")?.Get<RabbitMqOptions>();

            if (eventBusConfiguration != null && eventBusConfiguration.IsValid)
            {
                x.AddConsumer<LoginAttemptConsumer>();

                x.UsingRabbitMq((context, busConfig) =>
                {
                    busConfig.Host(eventBusConfiguration.Host, eventBusConfiguration.Port, eventBusConfiguration.VirtualHost, c => {
                        c.Username(eventBusConfiguration.Username);
                        c.Password(eventBusConfiguration.Password);
                    });

                    busConfig.ReceiveEndpoint(eventBusConfiguration.Queue.LoginQueue, endpoint =>
                    {
                        endpoint.ConfigureConsumer<LoginAttemptConsumer>(context);
                    });

                    busConfig.ConfigureEndpoints(context);
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
                var jwtConfiguration = config.GetSection("Jwt").Get<JwtOptions>();

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