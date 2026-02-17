using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Tamplate.Application.DTOs;
using Tamplate.Application.Interfaces.Services;

namespace Tamplate.Infrastructure.Services
{
    public class FileService:IFileService
    {
        private readonly string _baseUrl;
        private readonly string _webRootPath;

        public FileService(IWebHostEnvironment env, IConfiguration config)
        {
            _webRootPath = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");
            _baseUrl = config["APP_BASE_URL"] ?? "http://localhost:5068";
        }

        public virtual async Task<string> SaveFileAsync(IFormFile file, string subfolder, string[]? allowedExtensions = null)
        {
            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName);
            if (allowedExtensions != null && !allowedExtensions.Contains(extension.ToLower()))
                return null;

            var uploadPath = Path.Combine(_webRootPath, subfolder);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"{_baseUrl}/{subfolder.Replace("\\", "/")}/{uniqueFileName}";
            return fileUrl;
        }

        public virtual async Task<string> UpdateFileAsync(IFormFile newFile, string existingFilePath, string subfolder, string[]? allowedExtensions = null)
        {
            if (!string.IsNullOrEmpty(existingFilePath))
            {
                await DeleteFileAsync(existingFilePath, subfolder);
            }

            return await SaveFileAsync(newFile, subfolder, allowedExtensions);
        }

        public virtual async Task<bool> DeleteFileAsync(string fileUrl, string subfolder)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return false;

            var fileName = Path.GetFileName(fileUrl);
            var fullPath = Path.Combine(_webRootPath, subfolder, fileName);

            if (File.Exists(fullPath))
            {
                var fileInfo = new FileInfo(fullPath);
                await Task.Run(() => fileInfo.Delete());
                return true;
            }

            return false;
        }

        public virtual async Task<List<FileInfoDto>> GetAllFilesAsync(string? subfolder=null)
        {
            var targetFolder = string.IsNullOrEmpty(subfolder)
                ? _webRootPath
                : Path.Combine(_webRootPath, subfolder);

            if (!Directory.Exists(targetFolder))
            {
                return new List<FileInfoDto>();
            }
            var files = new List<FileInfoDto>();

            await Task.Run(() =>
            {
                var fileEntries = Directory.GetFiles(targetFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var filePath in fileEntries)
                {
                    var fileInfo = new FileInfo(filePath);
                    var relativePath = Path.GetRelativePath(_webRootPath, filePath).Replace("\\", "/");
                     files.Add(new FileInfoDto
                    {
                        Name = fileInfo.Name,
                        Url = $"{_baseUrl}/{relativePath}",
                        Size = fileInfo.Length,
                        Extension = fileInfo.Extension,
                        UploadDate = fileInfo.CreationTime,
                        Subfolder = subfolder ?? ""
                    });
                }
            });            
            return files;
        }

    }
}
