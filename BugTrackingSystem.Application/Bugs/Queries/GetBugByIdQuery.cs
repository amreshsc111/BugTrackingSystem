using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.Queries
{
    public record GetBugByIdQuery(Guid Id) : IRequest<BugDto?>;
}
