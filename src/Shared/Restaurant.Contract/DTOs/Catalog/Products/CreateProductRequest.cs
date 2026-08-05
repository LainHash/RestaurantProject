using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Catalog.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }

        public string? BrandId { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
    }
}
