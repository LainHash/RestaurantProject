using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Catalog
{
    internal class ProductCategoryRepository(RestaurantDbContext context) 
        : Repository<ProductCategory>(context), IProductCategoryRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<ProductCategory?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<ProductCategory?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }

        public async Task<ProductCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
