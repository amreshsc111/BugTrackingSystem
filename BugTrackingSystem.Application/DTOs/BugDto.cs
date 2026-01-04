using BugTrackingSystem.Domain.Enums;
using static BugTrackingSystem.Domain.Enums.GeneralEnums;

namespace BugTrackingSystem.Application.DTOs
{
    public record BugDto(
        Guid Id,
        string Title,
        string? Description,
        BugSeverity Severity,
        BugStatus Status,
        string? ReproductionSteps,
        Guid ReporterId,
        string? ReporterName,
        Guid? AssignedToId,
        string? AssignedToName,
        DateTime CreatedDate,
        List<BugAttachmentDto> Attachments
    );
}
