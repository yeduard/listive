using Microsoft.EntityFrameworkCore;
using Notifications.Database.Configurations;
using Notifications.Database.Models;

namespace Notifications.Database;

public class NotificationsContext : DbContext
{
    public DbSet<Notification> Notifications { get; set; }

    public DbSet<NotificationClassification> NotificationClassifications { get; set; }

    public NotificationsContext(DbContextOptions<NotificationsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new NotificationClassificationConfiguration());
        builder.ApplyConfiguration(new NotificationConfiguration());

        base.OnModelCreating(builder);
    }
}