using JobTracker.API.Entities;

namespace JobTracker.API.Services.Interfaces;

public interface ITokenService
{
  string GenerateToken(User user);
}