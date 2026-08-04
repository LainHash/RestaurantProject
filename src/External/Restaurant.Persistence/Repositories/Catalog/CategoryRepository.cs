using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Catalog
{
    internal class CategoryRepository(RestaurantDbContext context) 
        : Repository<Category>(context), ICategoryRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Category?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }
    }
}
