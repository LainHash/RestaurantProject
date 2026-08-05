using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class ProductSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Products.AnyAsync())
                return;

            var categories = await context.Categories
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();

            var categoriesDictionary = categories.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var brands = await context.Brands
                .ToListAsync();

            var brandsDictionary = brands.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<ProductRecord>("Products");


            foreach (var record in records)
            {
                Brand? brand = null;

                if (!categoriesDictionary.TryGetValue(record.CategoryName.ToLower(), out var category)) 
                    throw new Exception($"Category '{record.CategoryName}' not found.");

                if (!string.IsNullOrWhiteSpace(record.BrandName))
                {
                    brandsDictionary.TryGetValue(record.BrandName, out brand);
                }

                var product = _mapper.Map<Product>(record)
                    .SetCategory(category.Id)
                    .SetBrand(brand?.Id);

                context.Products.Add(product);
            }

            await context.SaveChangesAsync();
        }
    }
}
