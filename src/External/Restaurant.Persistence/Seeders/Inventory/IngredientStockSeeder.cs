using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Seeders.Inventory
{
    internal class IngredientStockSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.IngredientStocks.AnyAsync())
                return;

            var ingredients = await context.Ingredients
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();

            var ingredientDictionary = ingredients.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var branches = await context.Branches
                .Select(x => new { x.Id, x.Code })
                .ToListAsync();

            var branchDictionary = branches.ToDictionary(
                x => x.Code,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<IngredientStockRecord>("IngredientStocks");

            foreach (var record in records)
            {
                if (!ingredientDictionary.TryGetValue(record.IngredientName, out var ingredient))
                    throw new Exception($"Ingredient '{record.IngredientName}' not found.");

                if (!branchDictionary.TryGetValue(record.BranchCode, out var branch))
                    throw new Exception($"Branch '{record.BranchCode}' not found.");

                var ingredientStock = _mapper.Map<IngredientStock>(record)
                    .SetIngredient(ingredient.Id)
                    .SetBranch(branch.Id);

                context.IngredientStocks.Add(ingredientStock);
            }

            await context.SaveChangesAsync();
        }
    }
}
