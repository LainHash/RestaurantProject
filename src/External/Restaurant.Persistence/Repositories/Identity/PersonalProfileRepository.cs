using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Identity
{
    internal class PersonalProfileRepository(RestaurantDbContext context)
                : Repository<PersonalProfile>(context), IPersonalProfileRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<PersonalProfile?> FindByUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.PersonalProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }
    }
}
