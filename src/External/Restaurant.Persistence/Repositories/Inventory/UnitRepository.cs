using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Inventory
{
    internal class UnitRepository(RestaurantDbContext context)
        : Repository<Unit>(context), IUnitRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Unit?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Units.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }
    }
}
