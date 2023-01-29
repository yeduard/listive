namespace Notifications.Database.Models;

public class NotificationClassification
{
    public Guid NotificationClassificationId { get; protected set; }

    public string Name { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset UpdatedAt { get; protected set; }
    public DateTimeOffset DeletedAt { get; protected set; }

    protected NotificationClassification() { }

    public NotificationClassification(string name)
    {
        this.Name = name;
    }
}