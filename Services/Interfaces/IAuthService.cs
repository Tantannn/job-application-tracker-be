using JobTracker.API.DTOs.Auth;

namespace JobTracker.API.Services.Interfaces;

public interface IAuthService
{
  Task<AuthResponse> RegisterAsync(RegisterRequest request);
  Task<AuthResponse> LoginAsync(LoginRequest request);
}