using BugTrackingSystem.Application.Bugs.Commands;
using BugTrackingSystem.Application.Exceptions;
using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Domain.Entities;
using MediatR;

namespace BugTrackingSystem.Application.Bugs.CommandHandlers
{
    public class UpdateBugStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBugStatusCommand, bool>
    {
        public async Task<bool> Handle(UpdateBugStatusCommand command, CancellationToken cancellationToken)
        {
            var bug = await unitOfWork.Repository<Bug>().GetByIdAsync(command.BugId);
            if (bug == null)
            {
                throw new NotFoundException("Bug", command.BugId);
            }

            bug.Status = command.Request.Status;
            bug.ModifiedDate = DateTime.UtcNow;

            unitOfWork.Repository<Bug>().Update(bug);
            await unitOfWork.CompleteAsync();

            return true;
        }
    }
}
