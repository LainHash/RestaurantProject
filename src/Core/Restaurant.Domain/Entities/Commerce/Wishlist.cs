using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Entities.Commerce
{
    public partial class Wishlist : AuditableEntity
    {
        public string? SessionId { get; private set; }
        public int? CustomerId { get; private set; }
        public Customer? Customer { get; private set; } = null!;

        public ICollection<WishlistItem> WishlistItems { get; private set; } = [];
    }

    public partial class Wishlist
    {
        public Wishlist() { }

        public Wishlist(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
