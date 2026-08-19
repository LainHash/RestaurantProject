using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Guest
{
    internal class WalletRepository(RestaurantDbContext context) 
        : Repository<Wallet>(context), IWalletRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Wallet?> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Wallets.FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
        }
    }
}
