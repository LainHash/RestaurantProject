using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Repositories.Personnel
{
    internal class DepartmentRepository(RestaurantDbContext context)
        : Repository<Department>(context), IDepartmentRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<Department?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Department?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }

        public async Task<Department?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
