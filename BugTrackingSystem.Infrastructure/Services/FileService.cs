using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Application.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BugTrackingSystem.Infrastructure.Services
{
    public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
    {
        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            
            var fullPath = Path.Combine(webHostEnvironment.WebRootPath, filePath.TrimStart('/', '\\'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<string> SaveFileAsync(FileAttachmentDto file, string[] allowedExtensions)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");
            }

            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.Content.CopyToAsync(fileStream);
            }

            return $"uploads/{uniqueFileName}";
        }
    }
}
