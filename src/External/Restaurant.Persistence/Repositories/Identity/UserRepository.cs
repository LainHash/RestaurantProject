using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Identity
{
    internal class UserRepository(RestaurantDbContext context) 
        : Repository<User>(context), IUserRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
    }
}
