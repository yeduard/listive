using System;
using Microsoft.EntityFrameworkCore;
using Notifications.Database.Models;

namespace Notifications.Database.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<IEnumerable<Notification>> GetNotificationsPagedAsync(int page, int size);
}

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(NotificationsContext context) : base(context) { }

    public async Task<IEnumerable<Notification>> GetNotificationsPagedAsync(int page, int size)
    {
        return await _context.Notifications
            .OrderByDescending(x => x.CreatedAt)
            .Skip(size * page)
            .Take(size)
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }
}