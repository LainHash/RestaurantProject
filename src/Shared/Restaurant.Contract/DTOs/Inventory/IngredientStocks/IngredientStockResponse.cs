namespace Restaurant.Contract.DTOs.Inventory.IngredientStocks
{
    public class IngredientStockResponse
    {
        public string Id { get; set; } = string.Empty;

        public decimal QuantityOnHand { get; set; }

        public string BranchCode { get; set; } = string.Empty;
        public string IngredientName { get; set; } = string.Empty;
    }
}
