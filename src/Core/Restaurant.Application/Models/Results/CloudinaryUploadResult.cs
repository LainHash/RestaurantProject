namespace Restaurant.Application.Models.Results
{
    public class CloudinaryUploadResult
    {
        public bool IsSuccess { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;   // dùng để delete sau này
        public string StoragePath { get; set; } = string.Empty;   // = PublicId (e.g. "products/abc123")
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
