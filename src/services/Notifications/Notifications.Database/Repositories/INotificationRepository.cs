using System;
using Notifications.Database.Models;

namespace Notifications.Database.Repositories;

public interface INotificationRepository : IRepository<Notification>
{

}

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(NotificationsContext context) : base(context) { }
}