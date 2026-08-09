using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Seeders.Inventory
{
    internal class UnitSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Units.AnyAsync())
                return;

            var records =
                _importer.Read<UnitRecord>("Units");

            var entities =
                _mapper.Map<List<Unit>>(records);

            context.Units.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
