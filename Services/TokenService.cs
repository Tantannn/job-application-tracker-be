using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobTracker.API.Entities;
using JobTracker.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace JobTracker.API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
   public string GenerateToken(User user)
   {
     var key = new SymmetricSecurityKey(
       Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
     var claims = new List<Claim>
     {
       new(ClaimTypes.NameIdentifier, user.Id.ToString()),
       new(ClaimTypes.Email, user.Email)
     };
     var token = new JwtSecurityToken(
       issuer: config["Jwt:Issuer"],
       audience: config["Jwt:Audience"],
       claims: claims,
       expires: DateTime.UtcNow.AddDays(7),
       signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
     );
     return new JwtSecurityTokenHandler().WriteToken(token);
   }
}