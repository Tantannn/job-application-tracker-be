using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity.Data;

namespace JobTracker.API.Services.Interfaces;

public interface IAuthService
{
  Task<OAuthTokenResponse> RegisterAsync(RegisterRequest request);
  Task<OAuthTokenResponse> LoginAsync(LoginRequest request);
}