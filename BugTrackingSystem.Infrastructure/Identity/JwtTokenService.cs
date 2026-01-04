using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Common;
using BugTrackingSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTrackingSystem.Infrastructure.Identity
{
    public class JwtTokenService(IOptions<JwtConfig> jwtConfig) : IJwtTokenService
    {
        public string GenerateToken(User user)
        {
            var secret = jwtConfig.Value.Secret ?? throw new Exception("JWT Secret not configured");
            var expiryMinutes = int.TryParse(jwtConfig.Value.ExpiresMinutes, out var m) ? m : 60;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("canReportBugs", user.Roles.Any(r => r.Name == "Reporter" || r.Name == "Admin" || r.Name == "User") ? "true" : "false")
            };

            // Add role claims for authorization
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                issuer: jwtConfig.Value.Issuer,
                audience: jwtConfig.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
