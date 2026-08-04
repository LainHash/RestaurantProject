using Microsoft.Extensions.DependencyInjection;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Seeders.Catalog;

namespace Restaurant.Persistence.Seeders
{
    internal class DatabaseSeeder
    {
        private readonly RestaurantDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeeder(IServiceProvider serviceProvider, RestaurantDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            await SeedAsync<CategorySeeder>(_context);
            await SeedAsync<BrandSeeder>(_context);
        }

        private async Task SeedAsync<TSeeder>(RestaurantDbContext context) where TSeeder : IDataSeeder
        {
            using var scope = _serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
            await seeder.SeedAsync(context);
        }
    }
}
