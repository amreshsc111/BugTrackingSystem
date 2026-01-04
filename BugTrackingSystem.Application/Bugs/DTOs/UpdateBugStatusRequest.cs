using static BugTrackingSystem.Domain.Enums.GeneralEnums;

namespace BugTrackingSystem.Application.Bugs.DTOs
{
    public class UpdateBugStatusRequest
    {
        public BugStatus Status { get; set; }
    }
}
