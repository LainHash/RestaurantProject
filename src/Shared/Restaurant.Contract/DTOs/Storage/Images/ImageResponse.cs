namespace Restaurant.Contract.DTOs.Storage.Images
{
    public class ImageResponse
    {
        public string Id { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
}
