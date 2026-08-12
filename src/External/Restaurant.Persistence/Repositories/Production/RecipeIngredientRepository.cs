using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Repositories.Production;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Production
{
    internal class RecipeIngredientRepository(RestaurantDbContext context) 
        : Repository<RecipeIngredient>(context), IRecipeIngredientRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}
