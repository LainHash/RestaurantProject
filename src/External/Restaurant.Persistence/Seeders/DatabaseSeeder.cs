using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Seeders.Catalog;
using Restaurant.Persistence.Seeders.Inventory;
using Restaurant.Persistence.Seeders.Pricing;
using Restaurant.Persistence.Seeders.Storage;
using Restaurant.Persistence.Seeders.Territory;

namespace Restaurant.Persistence.Seeders
{
    internal class DatabaseSeeder(
        IServiceProvider serviceProvider,
        RestaurantDbContext context)
    {
        private readonly RestaurantDbContext _context = context;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task SeedAllAsync()
        {
            await SeedAsync<ProductCategorySeeder>(_context);
            await SeedAsync<IngredientCategorySeeder>(_context);
            await SeedAsync<BrandSeeder>(_context);

            await SeedAsync<UnitSeeder>(_context);

            await SeedAsync<BranchSeeder>(_context);

            await SeedAsync<ProductSeeder>(_context);
            await SeedAsync<ProductPriceSeeder>(_context);
            await SeedAsync<ProductStockSeeder>(_context);

            await SeedAsync<IngredientSeeder>(_context);
            await SeedAsync<IngredientPriceSeeder>(_context);

            await SeedAsync<ImageSeeder>(_context);
            await SeedAsync<ProductImageSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(RestaurantDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
