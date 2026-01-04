using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Application.Lists.Queries;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Application.Lists.QueryHandlers
{
    public class GetRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
    {
        public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await unitOfWork.Repository<Role>()
                .GetQueryable()
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync(cancellationToken);

            return roles;
        }
    }
}
