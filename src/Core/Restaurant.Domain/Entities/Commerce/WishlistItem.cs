using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Commerce
{
    public class WishlistItem : AuditableEntity
    {
        public int WishlistId { get; private set; }
        public Wishlist Wishlist { get; private set; } = null!;

        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
    }
}
