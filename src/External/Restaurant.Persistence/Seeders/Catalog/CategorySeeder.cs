using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Seeders.Catalog
{
    internal class CategorySeeder : IDataSeeder
    {
        private readonly IDataImporter _importer;
        private readonly IMapper _mapper;

        public CategorySeeder(
            IDataImporter importer,
            IMapper mapper)
        {
            _importer = importer;
            _mapper = mapper;
        }

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return;

            var records =
                _importer.Read<CategoryRecord>("Categories");

            var entities =
                _mapper.Map<List<Category>>(records);

            context.Categories.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
