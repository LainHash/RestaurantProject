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

            var records =
                _importer.Read<ProductPriceRecord>("ProductPrices");

            var entities =
                _mapper.Map<List<ProductPrice>>(records);

            context.ProductPrices.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
