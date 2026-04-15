using System.ComponentModel.DataAnnotations;

namespace JobTracker.API.DTOs.Auth;

public record RegisterRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password
);
