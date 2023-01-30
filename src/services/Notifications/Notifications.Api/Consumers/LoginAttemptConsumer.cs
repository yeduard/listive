using Listive.Messages;
using MassTransit;
using Notifications.Api.Services;

namespace Notifications.Api.Consumers;

public class LoginAttemptConsumer : IConsumer<LoginAttemptEvent>
{
    private readonly ILogger<LoginAttemptConsumer> _logger;
    private readonly INotificationService _notificationService;

    public LoginAttemptConsumer(ILogger<LoginAttemptConsumer> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<LoginAttemptEvent> context)
    {
        _logger.LogWarning("A new login was attempted for {Email} with {IpAddress} ip", context.Message.Email, context.Message.IpAddress);

        await _notificationService.GenerateLoginAttemptNotificationAsync(context.Message.Email, context.Message.IpAddress);
    }
}