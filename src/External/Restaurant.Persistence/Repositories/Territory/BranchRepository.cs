using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Territory
{
    internal class BranchRepository(RestaurantDbContext context)
        : Repository<Branch>(context), IBranchRepository
    {
        private readonly RestaurantDbContext _context = context;
        public async Task<Branch?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Branches.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }
    }
}
