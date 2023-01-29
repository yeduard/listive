using Notifications.Database.Repositories;

namespace Notifications.Database;

public interface IUnitOfWork
{
    INotificationRepository Notifications { get; }
    INotificationClassificationRepository NotificationClassifications { get; }

    Task<int> CommitAsync();
    void Dispose();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly NotificationsContext _context;

    private INotificationRepository _notificationRepository;
    private INotificationClassificationRepository _notificationClassificationRepository;

    public UnitOfWork(NotificationsContext context)
    {
        _context = context;
    }

    public INotificationRepository Notifications => _notificationRepository ??= new NotificationRepository(_context);

    public INotificationClassificationRepository NotificationClassifications => _notificationClassificationRepository ??= new NotificationClassificationRepository(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}