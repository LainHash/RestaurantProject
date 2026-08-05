using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class ProductResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }

        public string BrandName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
    }
}
