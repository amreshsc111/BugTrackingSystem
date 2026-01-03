using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public required string RefreshToken { get; set; }
    }
}
