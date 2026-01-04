using BugTrackingSystem.Application.Bugs.Commands;
using BugTrackingSystem.Application.Exceptions;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using BugTrackingSystem.Domain.Enums;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.CommandHandlers
{
    public class AssignBugCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AssignBugCommand, bool>
    {
        public async Task<bool> Handle(AssignBugCommand command, CancellationToken cancellationToken)
        {
            var bug = await unitOfWork.Repository<Bug>().GetByIdAsync(command.BugId);
            if (bug == null)
            {
                throw new NotFoundException("Bug", command.BugId);
            }

            var developer = await unitOfWork.Repository<User>().GetByIdAsync(command.DeveloperId);
            if (developer == null)
            {
                throw new NotFoundException("Developer", command.DeveloperId);
            }

            bug.AssignedToId = command.DeveloperId;
            bug.ModifiedDate = DateTime.UtcNow;
            
            // If status is Open, move to InProgress
            if (bug.Status == GeneralEnums.BugStatus.Open)
            {
                bug.Status = GeneralEnums.BugStatus.InProgress;
            }

            unitOfWork.Repository<Bug>().Update(bug);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
