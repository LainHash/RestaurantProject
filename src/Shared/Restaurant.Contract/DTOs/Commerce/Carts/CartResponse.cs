using Restaurant.Contract.DTOs.Commerce.CartItems;

namespace Restaurant.Contract.DTOs.Commerce.Carts
{
    public class CartResponse
    {
        public string Id { get; set; } = null!;

        public ICollection<CartItemResponse> CartItems { get; set; } = [];
    }
}
