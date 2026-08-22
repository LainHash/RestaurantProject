using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Commerce
{
    public class CartItem : AuditableEntity
    {
        public int CartId { get; private set; }
        public Cart Cart { get; private set; } = null!;

        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;

        public int Quantity { get; private set; }
    }
}
