using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.Queries
{
    public record GetUserBugsQuery(Guid UserId) : IRequest<List<BugDto>>;
}
