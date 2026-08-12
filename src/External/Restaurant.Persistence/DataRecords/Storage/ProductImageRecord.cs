namespace Restaurant.Persistence.DataRecords.Storage
{
    public class ProductImageRecord
    {
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
    }
}
