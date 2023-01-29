using Notifications.Database;
using Notifications.Database.Models;

namespace Notifications.Api.Services;

public interface INotificationService
{
    Task GenerateLoginAttemptNotification(string email, string ipAddress);
}

public class NotificationService : INotificationService
{
    private readonly ILogger<INotificationService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(ILogger<INotificationService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task GenerateLoginAttemptNotification(string email, string ipAddress)
    {
        _logger.LogInformation("Generating a new LoginAttemptNotification for {Email} with {IpAddress}", email, ipAddress);

        var classification = _unitOfWork.NotificationClassifications.Find(x => x.Name == "Email").FirstOrDefault();

        if (classification == null)
        {
            _logger.LogError("Unable to find the Email classification");
            return;
        }

        var newNotification = new Notification("New Login Attempt!", $"There was a new Login Attempt from {ipAddress}", email, classification);

        _logger.LogInformation("Saving new Notification");
        await _unitOfWork.Notifications.AddAsync(newNotification);

        try
        {
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("New notification created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("There was an error saving the new notification", ex);
        }
    }
}