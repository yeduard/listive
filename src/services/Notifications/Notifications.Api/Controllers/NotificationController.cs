using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Services;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly INotificationService _notificationService;

    public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    [Authorize]
    [HttpGet("paged")]
    public async Task<IActionResult> GetNotificationsPaged(int page, int size)
    {
        _logger.LogInformation("Getting Notifications paged");
        var notifications = await _notificationService.GetNotificationsPagedAsync(page, size);

        return Ok(notifications);
    }
}
