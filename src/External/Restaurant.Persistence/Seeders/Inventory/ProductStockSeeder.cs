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

            var products = await context.Products
                .Select(x => new {x.Id, x.Name})
                .ToListAsync();

            var productDictionary = products.ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

            var branches = await context.Branches
                .Select(x => new { x.Id, x.Code })
                .ToListAsync();

            var branchDictionary = branches.ToDictionary(
                x => x.Code,
                StringComparer.OrdinalIgnoreCase);

            var records =
                _importer.Read<ProductStockRecord>("ProductStocks");

            foreach (var record in records)
            {
                if (!productDictionary.TryGetValue(record.ProductName, out var product))
                    throw new Exception($"Product '{record.ProductName}' not found.");

                if(!branchDictionary.TryGetValue(record.BranchCode, out var branch))
                    throw new Exception($"Branch '{record.BranchCode}' not found.");

                var productStock = _mapper.Map<ProductStock>(record)
                    .SetProductId(product.Id)
                    .SetBranchId(branch.Id);

                context.ProductStocks.Add(productStock);
            }

            await context.SaveChangesAsync();
        }
    }
}
