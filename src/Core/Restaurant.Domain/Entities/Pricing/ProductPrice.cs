using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Pricing
{
    public class ProductPrice : SoftDeletableEntity
    {
        public decimal UnitPrice { get; private set; }

        public int ProductId { get; private set; }

        public Product Product { get; private set; } = null!;
    }
}
