using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Seeders
{
    internal interface IDataSeeder
    {
        Task SeedAsync(RestaurantDbContext context);
    }
}
