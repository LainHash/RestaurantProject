using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Catalog
{
    internal class IngredientRepository(RestaurantDbContext context)
        : Repository<Ingredient>(context), IIngredientRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Ingredients.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<Ingredient?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }

        public async Task<Ingredient?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
