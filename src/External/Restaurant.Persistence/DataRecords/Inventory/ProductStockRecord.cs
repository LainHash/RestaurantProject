namespace Restaurant.Persistence.DataRecords.Inventory
{
    internal class ProductStockRecord
    {
        public string Unit { get; set; } = null!;
        public decimal QuantityOnHand { get; set; }

        public string ProductName { get; set; } = null!;
    }
}
