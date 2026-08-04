using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Pricing
{
    public partial class ProductPrice : SoftDeletableEntity
    {
        public decimal UnitPrice { get; private set; }

        public int ProductId { get; private set; }

        public Product Product { get; private set; } = null!;
    }

    public partial class ProductPrice
    {
        public ProductPrice() { }
        public ProductPrice(decimal unitPrice, int productId)
        {
            UnitPrice = unitPrice;
            ProductId = productId;
        }
        
        public void SetProductId(int productId)
        {
            ProductId = productId;
        }
    }
}
