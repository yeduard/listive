namespace Login.Api.Configurations;

public record JwtConfiguration(string Key, string Issuer, string Audience, string Subject, int Expiration);