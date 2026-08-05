namespace Restaurant.Contract.DTOs.Inventory.ProductStocks
{
    public class ProductStockResponse
    {
        public string Id { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;
        public decimal QuantityOnHand { get; set; }

        public string BranchCode { get; set; } = string.Empty;
    }
}
