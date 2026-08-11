using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Production;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Production;

namespace Restaurant.Persistence.Seeders.Production
{
    internal class RecipeSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Recipes.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var productDictionary = products.ToDictionary(
                x => x.Name.ToLower(),
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<RecipeRecord>("Recipes");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductName, out var product))
                    throw new Exception($"Product '{record.ProductName}' not found.");

                var recipe = _mapper.Map<Recipe>(record)
                    .SetProduct(product.Id);

                context.Recipes.Add(recipe);
            }

            await context.SaveChangesAsync();
        }
    }
}
