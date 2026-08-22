using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Domain.Repositories.Personnel
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Department?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Department?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
