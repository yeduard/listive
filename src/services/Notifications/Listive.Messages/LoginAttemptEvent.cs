namespace Listive.Messages;

public record LoginAttemptEvent(string Email, string IpAddress);