using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using BugTrackingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BugTrackingSystem.Infrastructure.Identity
{
    public class RefreshTokenService(ApplicationDbContext context) : IRefreshTokenService
    {
        public async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId, string? createdByIp = null)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = createdByIp
            };

            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<bool> RevokeRefreshTokenAsync(string token)
        {
            var existingToken = await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (existingToken == null || existingToken.IsRevoked) return false;

            existingToken.IsRevoked = true;
            existingToken.ModifiedDate = DateTime.Now;
            existingToken.ModififedById = null;
            await context.SaveChangesAsync();

            return true;
        }
    }
}
