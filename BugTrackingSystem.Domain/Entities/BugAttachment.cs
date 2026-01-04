using BugTrackingSystem.Domain.Common;

namespace BugTrackingSystem.Domain.Entities
{
    public class BugAttachment : BaseEntity
    {
        public required string CustomName { get; set; }
        public required string OriginalName { get; set; }
        public required string FilePath { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }

        public Guid BugId { get; set; }
        public Bug Bug { get; set; } = null!;
    }
}
