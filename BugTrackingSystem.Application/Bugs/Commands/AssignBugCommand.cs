using MediatR;

namespace BugTrackingSystem.Application.Bugs.Commands
{
    public record AssignBugCommand(Guid BugId, Guid DeveloperId) : IRequest<bool>;
}
