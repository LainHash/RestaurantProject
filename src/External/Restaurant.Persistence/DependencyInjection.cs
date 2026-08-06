using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Application.Services.Inventory;
using Restaurant.Application.Services.Territory;
using Restaurant.Domain.Repositories;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Repositories;
using Restaurant.Persistence.Repositories.Catalog;
using Restaurant.Persistence.Seeders;
using Restaurant.Persistence.Services.Business;
using Restaurant.Persistence.Services.Catalog;
using Restaurant.Persistence.Services.Inventory;
using Restaurant.Persistence.Services.Territory;

namespace Restaurant.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ── Database ─────────────────────────────────────────────────────
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("MyConnectString"),
                    sqlOptions => sqlOptions.MigrationsAssembly(
                        typeof(RestaurantDbContext).Assembly.FullName)));

            // ── AutoMapper ───────────────────────────────────────────────────
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

            // ── Seeders ──────────────────────────────────────────────────────
            // Orchestrator seeder
            services.AddScoped<DatabaseSeeder>();

            // Auto-register all IDataSeeder implementations
            var seederTypes = typeof(DependencyInjection).Assembly.GetTypes()
                .Where(t => typeof(IDataSeeder).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in seederTypes)
            {
                services.AddScoped(type);
            }

            // ── Repositories ─────────────────────────────────────────────────
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var assembly = typeof(CategoryRepository).Assembly;

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract)
                    continue;

                if (!type.Name.EndsWith("Repository"))
                    continue;

                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.Name.EndsWith("Repository"))
                    {
                        services.AddScoped(iface, type);
                    }
                }
            }

            // ── Services ─────────────────────────────────────────────────────
            services.AddScoped<IDataImporter, ExcelImporter>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<IBranchService, BranchService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IProductStockService, ProductStockService>();
            return services;
        }

        public static async Task InitialiseDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;

            var context = sp.GetRequiredService<RestaurantDbContext>();
            await context.Database.MigrateAsync();

            var seeder = sp.GetRequiredService<DatabaseSeeder>();
            await seeder.SeedAllAsync();
        }
    }
}
