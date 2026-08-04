namespace Restaurant.Persistence.DataRecords.Pricing
{
    internal class ProductPriceRecord
    {
        public decimal UnitPrice { get; set; }

        public string ProductName { get; set; } = null!;
    }
}
