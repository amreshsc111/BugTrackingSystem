using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
