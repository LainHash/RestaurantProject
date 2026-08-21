using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Commerce
{
    internal class WishlistItemRepository(RestaurantDbContext context)
        : Repository<WishlistItem>(context), IWishlistItemRepository
    {
    }
}
