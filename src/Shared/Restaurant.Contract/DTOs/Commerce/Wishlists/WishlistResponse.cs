using Restaurant.Contract.DTOs.Commerce.WishlistItems;

namespace Restaurant.Contract.DTOs.Commerce.Wishlists
{
    public class WishlistResponse
    {
        public string Id { get; set; } = null!;

        public ICollection<WishlistItemResponse> WishlistItems { get; set; } = [];
    }
}