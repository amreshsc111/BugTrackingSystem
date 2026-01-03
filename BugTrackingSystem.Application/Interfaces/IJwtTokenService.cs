using BugTrackingSystem.Domain.Entities;

namespace BugTrackingSystem.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
