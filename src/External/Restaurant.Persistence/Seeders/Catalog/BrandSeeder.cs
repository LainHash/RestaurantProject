using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class BrandSeeder : IDataSeeder
    {
        private readonly IDataImporter _importer;
        private readonly IMapper _mapper;

        public BrandSeeder(
            IDataImporter importer,
            IMapper mapper)
        {
            _importer = importer;
            _mapper = mapper;
        }

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Brands.AnyAsync())
                return;

            var records =
                _importer.Read<BrandRecord>("Brands");

            var entities =
                _mapper.Map<List<Brand>>(records);

            context.Brands.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
