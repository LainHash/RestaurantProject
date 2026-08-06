using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Territory
{
    internal class BranchRepository(RestaurantDbContext context)
        : Repository<Branch>(context), IBranchRespository
    {
    }
}
