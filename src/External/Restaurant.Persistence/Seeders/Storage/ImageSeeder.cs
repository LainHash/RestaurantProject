using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Catalog;
using Restaurant.Persistence.DataRecords.Storage;

namespace Restaurant.Persistence.Seeders.Storage
{
    internal class ImageSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Images.AnyAsync())
                return;

            var records =
                _importer.Read<ImageRecord>("Images");

            var entities =
                _mapper.Map<List<Image>>(records);

            context.Images.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
