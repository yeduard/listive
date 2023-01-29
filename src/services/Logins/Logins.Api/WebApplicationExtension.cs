using Logins.Database;
using Microsoft.AspNetCore.Identity;

namespace Logins.Api;

public static class WebApplicationExtension
{
    private static string _defaultRootUsername = "rootie";
    private static string _defaultRootEmail = "rootie@root.com";
    private static string _defaultRootPassword = "Pass@word";

    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using(var serviceScope = app.Services.CreateScope())
        {
            var sp = serviceScope.ServiceProvider;

            var context = sp.GetRequiredService<LoginContext>();
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("Logins.DatabaseMigration");

            DbUtils.MigrateDatabase(context, logger);
        }

        return app;
    }

    public static WebApplication SeedIdentityService(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var sp = serviceScope.ServiceProvider;

            var userManager = sp.GetRequiredService<UserManager<IdentityUser>>();
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("Login.UserSeeder");

            logger.LogInformation("Seeding root user.");

            var rootUser = userManager.FindByEmailAsync(_defaultRootEmail).GetAwaiter().GetResult();

            if (rootUser == null)
            {
                logger.LogInformation("No root user found, creating a new one");

                rootUser = new IdentityUser
                {
                    UserName = _defaultRootUsername,
                    Email = _defaultRootEmail
                };

                var result = userManager.CreateAsync(rootUser, _defaultRootPassword).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    logger.LogInformation("A new root user was created");
                }
                else
                {
                    logger.LogWarning("Unable to create a new root user");
                }
            }
        }

        return app;
    }
}