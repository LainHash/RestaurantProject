using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class IngredientCategorySeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.IngredientCategories.AnyAsync())
                return;

            var records =
                _importer.Read<IngredientCategoryRecord>("IngredientCategories");

            var entities =
                _mapper.Map<List<IngredientCategory>>(records);

            context.IngredientCategories.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
