using BugTrackingSystem.Application.Bugs.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.Commands
{
    public record UpdateBugStatusCommand(Guid BugId, UpdateBugStatusRequest Request) : IRequest<bool>;
}
