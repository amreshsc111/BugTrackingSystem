using BugTrackingSystem.Application.Bugs.Queries;
using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Application.Bugs.QueryHandlers
{
    public class GetUserBugsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserBugsQuery, List<BugDto>>
    {
        public async Task<List<BugDto>> Handle(GetUserBugsQuery query, CancellationToken cancellationToken)
        {
            var q = unitOfWork.Repository<Bug>().GetQueryable();

            q = q.Where(b => b.ReporterId == query.UserId);

            return await q.Select(b => new BugDto(
                b.Id,
                b.Title,
                b.Description,
                b.Severity,
                b.Status,
                b.ReproductionSteps,
                b.ReporterId,
                b.Reporter.UserName,
                b.AssignedToId,
                b.AssignedTo != null ? b.AssignedTo.UserName : null,
                b.CreatedDate,
                b.Attachments.Select(a => new BugAttachmentDto(
                    a.Id,
                    a.CustomName,
                    a.OriginalName,
                    a.FilePath,
                    a.ContentType,
                    a.FileSize
                )).ToList()
            )).ToListAsync(cancellationToken);
        }
    }
}
