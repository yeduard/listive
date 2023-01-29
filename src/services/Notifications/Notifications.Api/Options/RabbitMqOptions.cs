namespace Notifications.Api.Options;

public record RabbitMqOptions
{
    public string? Host { get; set; }
    public string? VirtualHost { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public ushort Port { get; set; }
    public QueueOptions Queue { get; set; }

    public bool IsValid => !string.IsNullOrWhiteSpace(Host)
        && !string.IsNullOrWhiteSpace(VirtualHost)
        && !string.IsNullOrWhiteSpace(Username)
        && !string.IsNullOrWhiteSpace(Password)
        && Port != default;
}

public record QueueOptions(string LoginQueue);