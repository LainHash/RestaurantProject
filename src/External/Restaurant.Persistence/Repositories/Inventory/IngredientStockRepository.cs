using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Inventory;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Inventory
{
    internal class IngredientStockRepository(RestaurantDbContext context)
        : Repository<IngredientStock>(context), IIngredientStockRepository
    {
    }
}
