using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Guest
{
    internal class CustomerRepository(RestaurantDbContext context)
        : Repository<Customer>(context), ICustomerRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Customer?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Customer?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Customer?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }
    }
}
