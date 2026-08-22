using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Commerce
{
    internal class WishlistRepository(RestaurantDbContext context)
        : Repository<Wishlist>(context), IWishlistRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Wishlist?> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Wishlists
                .Include(x => x.WishlistItems)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }

        public async Task<Wishlist?> FindBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            return await _context.Wishlists
                .Include(x => x.WishlistItems)
                .FirstOrDefaultAsync(x => x.SessionId == sessionId, cancellationToken);
        }
    }
}
