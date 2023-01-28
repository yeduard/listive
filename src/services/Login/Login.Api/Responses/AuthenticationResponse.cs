namespace Login.Api.Responses;

public record AuthenticationResponse(string Token, DateTimeOffset Expiration);