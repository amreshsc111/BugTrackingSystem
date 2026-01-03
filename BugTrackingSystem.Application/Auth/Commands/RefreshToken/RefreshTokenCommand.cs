using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
