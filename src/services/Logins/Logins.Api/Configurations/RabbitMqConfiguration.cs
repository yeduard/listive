namespace Logins.Api.Configurations;

public record RabbitMqConfiguration
{
    public string? Host { get; set; }
    public string? VirtualHost { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public ushort Port { get; set; }

    public bool IsValid => !string.IsNullOrWhiteSpace(Host)
        && !string.IsNullOrWhiteSpace(VirtualHost)
        && !string.IsNullOrWhiteSpace(Username)
        && !string.IsNullOrWhiteSpace(Password)
        && Port != default;
}

