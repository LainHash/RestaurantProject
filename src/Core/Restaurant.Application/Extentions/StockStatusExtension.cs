using Restaurant.Domain.Enums;

namespace Restaurant.Application.Extentions
{
    public static class StockStatusExtensions
    {
        public const int LowStockThreshold = 10;

        public static StockStatus ToStockStatus(this decimal quantityOnHand) => quantityOnHand switch
        {
            0 => StockStatus.OutOfStock,
            < LowStockThreshold => StockStatus.LowStock,
            _ => StockStatus.InStock
        };
    }
}
