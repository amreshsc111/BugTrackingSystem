using MediatR;

namespace BugTrackingSystem.Application.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<Guid>
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }
    }
}
