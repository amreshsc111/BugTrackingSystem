using BugTrackingSystem.Application.DTOs;
using static BugTrackingSystem.Domain.Enums.GeneralEnums;

namespace BugTrackingSystem.Application.Bugs.DTOs
{
    public class CreateBugRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public BugSeverity Severity { get; set; }
        public string? ReproductionSteps { get; set; }
        public Guid? AssignedToId { get; set; }
        public List<FileAttachmentDto> Attachments { get; set; } = [];
    }
}
