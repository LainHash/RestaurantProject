using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Commerce
{
    internal class CartRepository(RestaurantDbContext context)
        : Repository<Cart>(context), ICartRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Cart?> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }

        public async Task<Cart?> FindBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.SessionId == sessionId, cancellationToken);
        }
    }
}
