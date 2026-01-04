using System;

namespace BugTrackingSystem.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
