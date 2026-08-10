namespace Restaurant.Persistence.DataRecords.Inventory
{
    internal class IngredientStockRecord
    {
        public decimal QuantityOnHand { get; set; }

        public string IngredientName { get; set; } = null!;
        public string BranchCode { get; set; } = null!;
    }
}
