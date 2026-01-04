using BugTrackingSystem.Application.Bugs.DTOs;
using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.Commands
{
    public record CreateBugCommand(CreateBugRequest Request, Guid ReporterId) : IRequest<Guid>;
}
