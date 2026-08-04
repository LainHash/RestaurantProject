using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Inventory
{
    public class ProductStock : SoftDeletableEntity
    {
        public string Unit { get; private set; } = string.Empty;
        public decimal QuantityOnHand { get; private set; }

        public int ProductId { get; private set; }

        public Product Product { get; private set; } = null!;
    }
}
