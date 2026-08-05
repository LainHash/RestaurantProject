using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Inventory
{
    internal class ProductStockRepository(RestaurantDbContext context) 
        : Repository<ProductStock>(context), IProductStockRepository
    {
    }
}
