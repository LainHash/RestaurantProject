using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Catalog
{
    internal class IngredientCategoryRepository(RestaurantDbContext context)
        : Repository<IngredientCategory>(context), IIngredientCategoryRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<IngredientCategory?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IngredientCategory?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }

        public async Task<IngredientCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.IngredientCategories.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
