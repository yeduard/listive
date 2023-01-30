namespace Notifications.Database.Models;

public class NotificationClassification
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; }

    public NotificationClassification() { }

    public NotificationClassification(string name)
    {
        this.Name = name;
    }
}