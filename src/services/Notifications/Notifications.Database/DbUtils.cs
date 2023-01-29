using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Notifications.Database;

public static class DbUtils
{
    public static Task MigrateDatabase(NotificationsContext context, ILogger logger)
    {
        logger.LogWarning("Validating if database have pending migrations.");

        try
        {
            bool databaseCreated = context.Database.EnsureCreated();

            if (!databaseCreated) return Task.CompletedTask;

            var pendingMigrations = context.Database.GetPendingMigrations();
            int pendingMigrationsCount = pendingMigrations.Count();

            logger.LogInformation("The database has {PendingMigrationsCount} pending migrations", pendingMigrationsCount);

            if (pendingMigrationsCount > 0)
            {
                context.Database.Migrate();
            }

            logger.LogInformation("Migrations applied to the database");
        }
        catch (Exception ex)
        {
            logger.LogError("There was an error migrating the database", ex);
        }

        return Task.CompletedTask;
    }
}