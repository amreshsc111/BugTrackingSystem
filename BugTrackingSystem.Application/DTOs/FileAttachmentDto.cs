namespace BugTrackingSystem.Application.DTOs
{
    public class FileAttachmentDto
    {
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required Stream Content { get; set; }
        public long Length { get; set; }
    }
}
