using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Role?> FindByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
