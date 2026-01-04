using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Application.Lists.Queries;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Application.Lists.QueryHandlers
{
    public class GetDevelopersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDevelopersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetDevelopersQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.Repository<User>()
                .GetQueryable()
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name == "Developer"))
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.UserName
                })
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
