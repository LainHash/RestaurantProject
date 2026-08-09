using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class ProductCategorySeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductCategories.AnyAsync())
                return;

            var records =
                _importer.Read<ProductCategoryRecord>("ProductCategories");

            var entities =
                _mapper.Map<List<ProductCategory>>(records);

            context.ProductCategories.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
