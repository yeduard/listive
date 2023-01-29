using System;
namespace Notifications.Database.Models;

public class Notification
{
    public Guid NotificationId { get; protected set; }

    public string Title { get; protected set; }
    public string Message { get; protected set; }
    public string Email { get; protected set; }
    public NotificationClassification Classification { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset UpdatedAt { get; protected set; }
    public DateTimeOffset DeletedAt { get; protected set; }

    protected Notification() { }

    public Notification(string title, string message, string email, NotificationClassification classification)
    {
        this.Title = title;
        this.Message = message;
        this.Email = email;
        this.Classification = classification;
    }
}