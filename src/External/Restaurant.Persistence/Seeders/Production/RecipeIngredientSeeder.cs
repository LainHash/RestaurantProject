using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Production;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Pricing;
using Restaurant.Persistence.DataRecords.Production;

namespace Restaurant.Persistence.Seeders.Production
{
    internal class RecipeIngredientSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.RecipeIngredients.AnyAsync())
                return;

            var ingredients = await context.Ingredients
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var ingredientsDictionary = ingredients.ToDictionary(
                x => x.Name.ToLower(),
                StringComparer.OrdinalIgnoreCase);

            var recipes = await context.Recipes
                .Include(x => x.Product)
                .Select(x => new { x.Id, x.Product.Name })
                .ToListAsync();
            var recipeDictionary = recipes.ToDictionary(
                x => x.Name.ToLower(),
                StringComparer.OrdinalIgnoreCase);

            var units = await context.Units
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var unitDictionary = units.ToDictionary(
                x => x.Name.ToLower(),
                StringComparer.OrdinalIgnoreCase);
            

            var records =
                _importer.Read<RecipeIngredientRecord>("RecipeIngredients");

            foreach (var record in records)
            {
                if (!ingredientsDictionary.TryGetValue(record.IngredientName, out var ingredient))
                    throw new Exception($"Ingredient '{record.IngredientName}' not found.");

                if (!recipeDictionary.TryGetValue(record.ProductName, out var recipe))
                    throw new Exception($"Recipe '{record.ProductName}' not found.");

                if (!unitDictionary.TryGetValue(record.UnitName.ToLower(), out var unit))
                    throw new Exception($"Unit '{record.UnitName}' not found.");

                var recipeIngredient = _mapper.Map<RecipeIngredient>(record)
                    .SetIngredient(ingredient.Id)
                    .SetRecipe(recipe.Id)
                    .SetUnit(unit.Id);

                context.RecipeIngredients.Add(recipeIngredient);
            }

            await context.SaveChangesAsync();
        }
    }
}
