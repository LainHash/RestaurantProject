using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Storage;

namespace Restaurant.Persistence.Seeders.Storage
{
    internal class ProductImageSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductImages.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            var productDictionary = products.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var images = await context.Images
                .Select(x => new {x.Id, x.AltText})
                .ToListAsync();
            var imageDictionary = images.ToDictionary(
                x => x.AltText,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<ProductImageRecord>("ProductImages");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductName.ToLower(), out var product))
                    throw new Exception($"Product '{record.ProductName}' not found.");

                if (!imageDictionary.TryGetValue(record.AltText.ToLower(), out var image))
                    throw new Exception($"Image '{record.AltText}' not found.");

                var productImage = _mapper.Map<ProductImage>(record)
                    .SetProduct(product.Id)
                    .SetImage(image.Id);

                context.ProductImages.Add(productImage);
            }

            await context.SaveChangesAsync();
        }
    }
}
