using BugTrackingSystem.Domain.Common;

namespace BugTrackingSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public ICollection<Role> Roles { get; set; } = [];
    }
}
