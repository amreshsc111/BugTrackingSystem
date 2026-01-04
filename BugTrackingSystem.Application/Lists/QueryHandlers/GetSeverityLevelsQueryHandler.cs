using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Application.Lists.Queries;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Application.Lists.QueryHandlers
{
    public class GetSeverityLevelsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSeverityLevelsQuery, IEnumerable<SeverityLevelDto>>
    {
        public async Task<IEnumerable<SeverityLevelDto>> Handle(GetSeverityLevelsQuery request, CancellationToken cancellationToken)
        {
            var severityLevels = await unitOfWork.Repository<SeverityLevel>()
                .GetQueryable()
                .Select(s => new SeverityLevelDto
                {
                    Id = s.Id,
                    Name = s.Name ?? string.Empty
                })
                .ToListAsync(cancellationToken);

            return severityLevels;
        }
    }
}
