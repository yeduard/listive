namespace Notifications.Database.Models;

public class Notification
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Message { get; set; }
    public string Email { get; set; }

    public Guid ClassificationId { get; set; }
    public NotificationClassification Classification { get; protected set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; }

    protected Notification() { }

    public Notification(string title, string message, string email, NotificationClassification classification)
    {
        this.Title = title;
        this.Message = message;
        this.Email = email;
        this.Classification = classification;
    }
}