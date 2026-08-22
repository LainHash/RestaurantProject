using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Commerce
{
    public partial class WishlistItem : AuditableEntity
    {
        public int WishlistId { get; private set; }
        public Wishlist Wishlist { get; private set; } = null!;

        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
    }

    public partial class WishlistItem
    {
        public WishlistItem() { }

        public WishlistItem(int wishlistId, int productId)
        {
            WishlistId = wishlistId;
            ProductId = productId;
        }
    }
}
