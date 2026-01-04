namespace BugTrackingSystem.Application.DTOs
{
    public record BugAttachmentDto(
        Guid Id,
        string CustomName,
        string OriginalName,
        string FilePath,
        string? ContentType,
        long FileSize
    );
}
