using Microsoft.AspNetCore.Http;
using Tamplate.Application.DTOs;

namespace Tamplate.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string subfolder, string[] allowedExtensions);
        Task<string> UpdateFileAsync(IFormFile newFile, string existingFilePath, string subfolder, string[]? allowedExtensions = null);
        Task<bool> DeleteFileAsync(string fileUrl, string subfolder);
        Task<List<FileInfoDto>>GetAllFilesAsync(string? subfolder);
    }
}
