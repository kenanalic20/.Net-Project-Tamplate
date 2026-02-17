using Microsoft.AspNetCore.Http;

namespace Tamplate.Application.DTOs
{
    public class FileDto
    {
        public IFormFile? File { get; set; }
        public string Subfolder { get; set; } = "General";
    }
    public class FileUpdateDto : FileDto
    {
        public string? OldFileUrl { get; set; }
    }
    public class FileDeleteDto
    {
        public string FileUrl { get; set; } = string.Empty;
        public string Subfolder { get; set; } = "General";
    }
    public class FileInfoDto
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public long? Size { get; set; }
        public string? Extension { get; set; }
        public DateTime? UploadDate { get; set; } 
        public string? Subfolder { get; set; }
    }
}
