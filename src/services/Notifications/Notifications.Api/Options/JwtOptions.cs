namespace Notifications.Api.Options;

public record JwtOptions(string Key, string Issuer, string Audience, string Subject, int Expiration);