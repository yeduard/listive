using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logins.Database;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddLoginDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<LoginContext>(options => {
            options.UseSqlServer(config.GetConnectionString("SqlServer"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<LoginContext>();

        return services;
    }
}

