namespace Logins.Api.Responses;

public record AuthenticationResponse(string Token, DateTimeOffset Expiration);