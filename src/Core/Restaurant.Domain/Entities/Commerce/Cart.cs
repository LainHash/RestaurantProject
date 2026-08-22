using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Entities.Commerce
{
    public partial class Cart : AuditableEntity
    {
        public string? SessionId { get; private set; }
        public int? CustomerId { get; private set; }
        public Customer? Customer { get; private set; } = null!;

        public ICollection<CartItem> CartItems { get; private set; } = [];
    }

    public partial class Cart
    {
        public Cart() { }

        public Cart(int customerId)
        {
            CustomerId = customerId;
        }

        public Cart(string sessionId)
        {
            SessionId = sessionId;
        }

        public void Merge(Cart source)
        {
            foreach (var sourceItem in source.CartItems)
            {
                var existingCartItem = CartItems.FirstOrDefault(x => x.ProductId == sourceItem.ProductId);
                if (existingCartItem is not null)
                {
                    existingCartItem.UpdateQuantity(sourceItem.Quantity);
                }

                CartItems.Add(new CartItem(this.Id, sourceItem.ProductId));
            }
        }
    }
}
