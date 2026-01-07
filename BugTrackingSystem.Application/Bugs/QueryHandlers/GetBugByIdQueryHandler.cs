using BugTrackingSystem.Application.Bugs.Queries;
using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Application.Bugs.QueryHandlers
{
    public class GetBugByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBugByIdQuery, BugDto?>
    {
        public async Task<BugDto?> Handle(GetBugByIdQuery query, CancellationToken cancellationToken)
        {
            var bug = await unitOfWork.Repository<Bug>().GetQueryable()
                .Include(b => b.Reporter)
                .Include(b => b.AssignedTo)
                .Include(b => b.Attachments)
                .FirstOrDefaultAsync(b => b.Id == query.Id, cancellationToken);

            if (bug == null) return null;

            return new BugDto(
                bug.Id,
                bug.Title,
                bug.Description,
                bug.Severity,
                bug.Status,
                bug.ReproductionSteps,
                bug.ReporterId,
                bug.Reporter.UserName,
                bug.AssignedToId,
                bug.AssignedTo?.UserName,
                bug.CreatedDate,
                bug.Attachments.Select(a => new BugAttachmentDto(
                    a.Id,
                    a.CustomName,
                    a.OriginalName,
                    a.FilePath,
                    a.ContentType,
                    a.FileSize
                )).ToList()
            );
        }
    }
}
