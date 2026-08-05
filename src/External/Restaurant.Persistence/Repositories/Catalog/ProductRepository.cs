using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Catalog
{
    internal class ProductRepository(RestaurantDbContext context)
        : Repository<Product>(context), IProductRepository
    {
    }
}
