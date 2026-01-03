using BugTrackingSystem.Domain.Common;
using static BugTrackingSystem.Domain.Enums.GeneralEnums;

namespace BugTrackingSystem.Domain.Entities
{
    public class Bug : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public BugSeverity Severity { get; set; }
        public BugStatus Status { get; set; } = BugStatus.Open;
        public string? ReproductionSteps { get; set; }

        public Guid ReporterId { get; set; }

        public Guid? AssignedToId { get; set; }
    }
}
