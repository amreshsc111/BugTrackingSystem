using BugTrackingSystem.Application.Bugs.Commands;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.CommandHandlers
{
    public class CreateBugCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<CreateBugCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBugCommand command, CancellationToken cancellationToken)
        {
            var attachments = new List<BugAttachment>();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".log", ".txt", ".docx" };

            if (command.Request.Attachments != null)
            {
                foreach (var file in command.Request.Attachments)
                {
                    var savedPath = await fileService.SaveFileAsync(file, allowedExtensions);
                    attachments.Add(new BugAttachment
                    {
                        CustomName = file.FileName,
                        OriginalName = file.FileName,
                        FilePath = savedPath,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        CreatedById = command.ReporterId,
                        CreatedDate = DateTime.UtcNow
                    });
                }
            }

            var bug = new Bug
            {
                Title = command.Request.Title,
                Description = command.Request.Description,
                Severity = command.Request.Severity,
                ReproductionSteps = command.Request.ReproductionSteps,
                ReporterId = command.ReporterId,
                AssignedToId = command.Request.AssignedToId,
                Status = command.Request.AssignedToId.HasValue 
                    ? Domain.Enums.GeneralEnums.BugStatus.InProgress 
                    : Domain.Enums.GeneralEnums.BugStatus.Open,
                Attachments = attachments,
                CreatedById = command.ReporterId,
                CreatedDate = DateTime.UtcNow
            };

            await unitOfWork.Repository<Bug>().AddAsync(bug);
            await unitOfWork.CompleteAsync();

            return bug.Id;
        }
    }
}
