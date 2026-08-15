using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Services.Business;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.DataRecords.Identity;

namespace Restaurant.Persistence.Seeders.Identity
{
    internal class RoleSeeder(
        IDataImporter importer,
        IMapper mapper) : IDataSeeder
    {
        private readonly IDataImporter _importer = importer;
        private readonly IMapper _mapper = mapper;

        public async Task SeedAsync(RestaurantDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var records =
                _importer.Read<RoleRecord>("Roles");

            var entities =
                _mapper.Map<List<Role>>(records);

            context.Roles.AddRange(entities);

            await context.SaveChangesAsync();
        }
    }
}
