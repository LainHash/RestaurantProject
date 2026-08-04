using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Product : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public InventoryType InventoryType { get; private set; }

        public int? BrandId { get; private set; }
        public int CategoryId { get; private set; }

        public Brand? Brand { get; private set; }
        public Category Category { get; private set; } = null!;
        
        public ProductPrice ProductPrice { get; private set; } = null!;
        public ICollection<ProductStock> ProductStocks { get; private set; } = [];
    }
}
