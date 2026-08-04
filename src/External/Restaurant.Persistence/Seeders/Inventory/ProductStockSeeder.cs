using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Seeders.Inventory
{
    internal class ProductStockSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.ProductStocks.AnyAsync())
                return;

            var records =
                _importer.Read<ProductStockRecord>("ProductStocks");

            var entities =
                _mapper.Map<List<ProductStock>>(records);

            context.ProductStocks.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
