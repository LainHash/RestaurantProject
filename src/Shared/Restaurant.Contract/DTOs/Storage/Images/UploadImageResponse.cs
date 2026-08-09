namespace Restaurant.Contract.DTOs.Storage.Images
{
    public class UploadImageResponse
    {
        public string ImageId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
        public decimal FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
    }
}
