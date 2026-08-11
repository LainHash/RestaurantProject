using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Abstraction;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Entities.Territory;
using System.Reflection;

namespace Restaurant.Persistence.Context
{
    internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : DbContext(options)
    {
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<IngredientCategory> IngredientCategories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Unit> Units { get; set; } = null!;

        public DbSet<Branch> Branches { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductPrice> ProductPrices { get; set; } = null!;
        public DbSet<ProductStock> ProductStocks { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;

        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<IngredientPrice> IngredientPrices { get; set; } = null!;
        public DbSet<IngredientStock> IngredientStocks { get; set; } = null!;

        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;

        // ── Model building ──────────────────────────────────────────────────
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Auto-register all IEntityTypeConfiguration<T> classes in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // ── Auto-set audit fields on SaveChanges ────────────────────────────
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.MarkCreated(now);
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.MarkUpdated(now);
                }
            }
        }
    }
}
