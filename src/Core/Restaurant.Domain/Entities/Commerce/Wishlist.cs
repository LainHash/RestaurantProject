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

        public Wishlist(string sessionId)
        {
            SessionId = sessionId;
        }

        public void Merge(Wishlist wishlist)
        {
            foreach (var sourceItem in wishlist.WishlistItems)
            {
                if (WishlistItems.Any(x => x.ProductId == sourceItem.ProductId))
                    continue;

                WishlistItems.Add(new WishlistItem(this.Id, sourceItem.ProductId));
            }
        }
    }
}
