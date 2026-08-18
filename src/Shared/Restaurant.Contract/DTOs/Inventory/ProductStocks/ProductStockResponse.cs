using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Inventory.ProductStocks
{
    public class ProductStockResponse
    {
        public string Id { get; set; } = string.Empty;

        public decimal QuantityOnHand { get; set; }

        public StockStatus Status { get; set; }

        public string BranchCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
}
