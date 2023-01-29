using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Notifications.Database;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddNotificationsDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<NotificationsContext>(options => {
            options.UseSqlServer(config.GetConnectionString("SqlServer"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}