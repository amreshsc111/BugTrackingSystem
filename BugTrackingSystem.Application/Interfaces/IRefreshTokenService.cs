using BugTrackingSystem.Domain.Entities;

namespace BugTrackingSystem.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId, string? createdByIp = null);
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
