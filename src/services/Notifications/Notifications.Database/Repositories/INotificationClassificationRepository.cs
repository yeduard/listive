using Notifications.Database.Models;

namespace Notifications.Database.Repositories;

public interface INotificationClassificationRepository : IRepository<NotificationClassification>
{
}

public class NotificationClassificationRepository : Repository<NotificationClassification>, INotificationClassificationRepository
{
    public NotificationClassificationRepository(NotificationsContext context) : base(context) { }
}