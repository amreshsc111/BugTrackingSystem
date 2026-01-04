namespace BugTrackingSystem.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? ModififedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
