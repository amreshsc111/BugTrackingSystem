using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using System.Security.Claims;

namespace BugTrackingSystem.Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService) : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Verify Refresh Token exists and is valid
            // Since IRefreshTokenService logic might handle retrieval/verification, let's see. 
            // Previous check showed GenerateRefreshTokenAsync and RevokeRefreshTokenAsync in IRefreshTokenService.
            // It doesn't seem to have "Validate" or "Get".
            // So checking manually against DB via UnitOfWork.

            var refreshTokens = await unitOfWork.Repository<BugTrackingSystem.Domain.Entities.RefreshToken>().GetAllAsync();
            var storedRefreshToken = refreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);

            if (storedRefreshToken == null || 
                storedRefreshToken.Expires < DateTime.UtcNow || 
                storedRefreshToken.IsRevoked)
            {
                 throw new Exception("Invalid refresh token");
            }

            // Revoke old
            await refreshTokenService.RevokeRefreshTokenAsync(request.RefreshToken);

            // Get User
            var user = await unitOfWork.Repository<User>().GetByIdAsync(storedRefreshToken.UserId);
            if (user == null) throw new Exception("User not found");

            // Generate new
            var newToken = jwtTokenService.GenerateToken(user);
            var newRefreshToken = await refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                Token = newToken,
                RefreshToken = newRefreshToken.Token,
                Expiration = newRefreshToken.Expires
            };
        }
    }
}
