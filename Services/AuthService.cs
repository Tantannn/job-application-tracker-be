using JobTracker.API.DTOs.Auth;
using JobTracker.API.Entities;
using JobTracker.API.Repositories.Interfaces;
using JobTracker.API.Services.Interfaces;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
using RegisterRequest = Microsoft.AspNetCore.Identity.Data.RegisterRequest;

namespace JobTracker.API.Services;


public class AuthService(
    IUserRepository userRepo,
    ITokenService tokenService) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await userRepo.ExistsAsync(request.Email))
            throw new InvalidOperationException("Email already registered.");

        var user = new User()
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await userRepo.CreateAsync(user);
        var token = tokenService.GenerateToken(user);

        return new AuthResponse(user.Id, user.Email, token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepo.GetByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        var token = tokenService.GenerateToken(user);

        return new AuthResponse(user.Id, user.Email, token);
    }
}
