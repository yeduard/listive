using Notifications.Database;

namespace Notifications.Api;

public static class WebApplicationExtension
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var sp = serviceScope.ServiceProvider;

            var context = sp.GetRequiredService<NotificationsContext>();
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("Logins.DatabaseMigration");

            DbUtils.MigrateDatabase(context, logger);
        }

        return app;
    }
}