using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Territory;

namespace Restaurant.Persistence.Seeders.Territory
{
    internal class BranchSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Branches.AnyAsync())
                return;

            var records =
                _importer.Read<BranchRecord>("Branches");

            var entities =
                _mapper.Map<List<Branch>>(records);

            context.Branches.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
