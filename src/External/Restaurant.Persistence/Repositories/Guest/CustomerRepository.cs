using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Guest
{
    internal class CustomerRepository(RestaurantDbContext context) 
        : Repository<Customer>(context), ICustomerRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
