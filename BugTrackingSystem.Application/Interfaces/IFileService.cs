using BugTrackingSystem.Application.DTOs;

namespace BugTrackingSystem.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(FileAttachmentDto file, string[] allowedExtensions);
        void DeleteFile(string filePath);
    }
}
