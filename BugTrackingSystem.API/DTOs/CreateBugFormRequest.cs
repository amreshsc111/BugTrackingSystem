using BugTrackingSystem.Domain.Enums;
using static BugTrackingSystem.Domain.Enums.GeneralEnums;

namespace BugTrackingSystem.API.DTOs
{
    public class CreateBugFormRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public BugSeverity Severity { get; set; }
        public string? ReproductionSteps { get; set; }
        public Guid? AssignedToId { get; set; }
        public List<IFormFile> Attachments { get; set; } = [];
    }
}
