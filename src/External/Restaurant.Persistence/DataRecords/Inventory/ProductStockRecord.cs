namespace Restaurant.Persistence.DataRecords.Inventory
{
    internal class ProductStockRecord
    {
        public decimal QuantityOnHand { get; set; }

        public string ProductName { get; set; } = null!;
        public string BranchCode { get; set; } = null!;
    }
}
