using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Services.Business;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Seeders;
using Restaurant.Persistence.Services.Business;

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

            // ── Services ─────────────────────────────────────────────────────
            services.AddScoped<IDataImporter, ExcelImporter>();

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
