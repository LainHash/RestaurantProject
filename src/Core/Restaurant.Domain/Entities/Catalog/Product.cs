using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.Catalog
{
    public partial class Product : SoftDeletableEntity
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
        public ICollection<ProductImage> ProductImages { get; private set; } = [];
    }

    public partial class Product
    {
        public Product SetBrand(int? brandId)
        {
            BrandId = brandId;
            return this;
        }

        public Product SetCategory(int categoryId)
        {
            CategoryId = categoryId;
            return this;
        }
    }
}
