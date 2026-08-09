using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Catalog
{
    internal class ProductRepository(RestaurantDbContext context)
        : Repository<Product>(context), IProductRepository
    {
        private readonly RestaurantDbContext _context = context;
        public async Task<Product?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }
    }
}
