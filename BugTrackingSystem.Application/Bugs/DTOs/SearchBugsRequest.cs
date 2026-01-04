using static BugTrackingSystem.Domain.Enums.GeneralEnums;

namespace BugTrackingSystem.Application.Bugs.DTOs
{
    public class SearchBugsRequest
    {
        public string? SearchTerm { get; set; }
        public BugStatus? Status { get; set; }
        public Guid? AssignedToId { get; set; }
    }
}
