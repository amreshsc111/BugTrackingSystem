using BugTrackingSystem.Application.Bugs.DTOs;
using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.Queries
{
    public record SearchBugsQuery(SearchBugsRequest Request) : IRequest<List<BugDto>>;
}
