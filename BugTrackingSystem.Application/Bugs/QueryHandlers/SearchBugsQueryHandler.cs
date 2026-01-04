using BugTrackingSystem.Application.Bugs.Queries;
using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Application.Bugs.QueryHandlers
{
    public class SearchBugsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<SearchBugsQuery, List<BugDto>>
    {
        public async Task<List<BugDto>> Handle(SearchBugsQuery query, CancellationToken cancellationToken)
        {
            var request = query.Request;
            var q = unitOfWork.Repository<Bug>().GetQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.ToLower();
                q = q.Where(b => b.Title.ToLower().Contains(term) || (b.Description != null && b.Description.ToLower().Contains(term)));
            }
            if (request.Status.HasValue)
            {
                q = q.Where(b => b.Status == request.Status);
            }
            if (request.AssignedToId.HasValue)
            {
                q = q.Where(b => b.AssignedToId == request.AssignedToId);
            }

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
