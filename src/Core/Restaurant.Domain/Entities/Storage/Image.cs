using Restaurant.Domain.Abstraction;

namespace Restaurant.Domain.Entities.Storage
{
    public partial class Image : AuditableEntity
    {
        public string AltText { get; private set; } = null!;

        public string Url { get; private set; } = null!;
        public string StoragePath { get; private set; } = null!;

        public decimal FileSize { get; private set; }
        public string ContentType { get; private set; } = null!;

        public ProductImage ProductImage { get; private set; } = null!;
    }

    public partial class Image
    {
        public Image()
        {

        }

        public Image(string altText, string url, string storagePath, long fileSize, string contentType)
        {
            AltText = altText;
            Url = url;
            StoragePath = storagePath;
            FileSize = fileSize;
            ContentType = contentType;
        }

        public static Image Create(string altText, string url, string storagePath, long fileSize, string contentType)
        {
            return new Image(altText, url, storagePath, fileSize, contentType);
        }
    }
}
