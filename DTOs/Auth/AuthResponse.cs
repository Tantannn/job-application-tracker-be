namespace JobTracker.API.DTOs.Auth;

public record AuthResponse(
    int Id,
    string Email,
    string Token
);
