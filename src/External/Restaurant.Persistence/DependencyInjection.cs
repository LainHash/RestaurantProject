using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Application.Services.Guest;
using Restaurant.Application.Services.Identity;
using Restaurant.Application.Services.Inventory;
using Restaurant.Application.Services.Production;
using Restaurant.Application.Services.Storage;
using Restaurant.Application.Services.Territory;
using Restaurant.Domain.Repositories;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Repositories;
using Restaurant.Persistence.Repositories.Catalog;
using Restaurant.Persistence.Seeders;
using Restaurant.Persistence.Services.Auth;
using Restaurant.Persistence.Services.Business;
using Restaurant.Persistence.Services.Catalog;
using Restaurant.Persistence.Services.Guest;
using Restaurant.Persistence.Services.Identity;
using Restaurant.Persistence.Services.Inventory;
using Restaurant.Persistence.Services.Production;
using Restaurant.Persistence.Services.Storage;
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

            var assembly = typeof(ProductCategoryRepository).Assembly;

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

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IIngredientCategoryService, IngredientCategoryService>();
            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<IBranchService, BranchService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IIngredientService, IngredientService>();

            services.AddScoped<IProductStockService, ProductStockService>();
            services.AddScoped<IIngredientStockService, IngredientStockService>();

            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IRecipeService, RecipeService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IOtpVerificationService, OtpVerificationService>();
            services.AddScoped<IPersonalProfileService, PersonalProfileService>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IWalletService, WalletService>();

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
