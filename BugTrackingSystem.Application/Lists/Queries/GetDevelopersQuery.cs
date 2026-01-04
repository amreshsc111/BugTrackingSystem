using BugTrackingSystem.Application.DTOs;
using MediatR;

namespace BugTrackingSystem.Application.Lists.Queries
{
    public record GetDevelopersQuery() : IRequest<List<UserDto>>;
}
