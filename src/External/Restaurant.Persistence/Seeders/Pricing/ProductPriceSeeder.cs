using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Pricing;

namespace Restaurant.Persistence.Seeders.Pricing
{
    internal class ProductPriceSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductPrices.AnyAsync())
                return;

            var products = await context.Products
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();

            var productDictionary = products.ToDictionary(
                x => x.Name.ToLower(),
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<ProductPriceRecord>("ProductPrices");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductName, out var product))
                    throw new Exception($"Product '{record.ProductName}' not found.");

                var price = _mapper.Map<ProductPrice>(record);
                price.SetProductId(product.Id);

                context.ProductPrices.Add(price);
            }

            await context.SaveChangesAsync();
        }
    }
}
