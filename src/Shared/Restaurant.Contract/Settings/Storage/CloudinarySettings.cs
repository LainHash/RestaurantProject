namespace Restaurant.Contract.Settings.Storage
{
    public class CloudinarySettings
    {
        public const string SectionName = "Cloudinary";

        public string CloudName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
        public string Folder { get; set; } = "Products";
    }
}
