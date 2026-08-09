namespace Restaurant.Persistence.DataRecords.Storage
{
    public class ImageRecord
    {
        public string AltText { get; set; } = null!;

        public string Url { get; set; } = null!;
        public string StoragePath { get; set; } = null!;

        public decimal FileSize { get; set; }
        public string ContentType { get; set; } = null!;
    }
}
