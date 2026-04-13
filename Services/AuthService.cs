using JobTracker.API.Repositories.Interfaces;
using JobTracker.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity.Data;

namespace JobTracker.API.Services;


public class AuthService(
    IUserRepository userRepo,
    ITokenService tokenService) : IAuthService
{

    public async Task<OAuthTokenResponse> RegisterAsync(RegisterRequest request)
    {
        
        
        return new OAuthTokenResponse(user.Id, user.Email, token);
    }
}