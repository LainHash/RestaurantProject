namespace Restaurant.Contract.DTOs.Storage.Images
{
    public class UploadImageRequest
    {
        public string? AltText { get; set; }
        public bool IsPrimary { get; set; }
    }
}
