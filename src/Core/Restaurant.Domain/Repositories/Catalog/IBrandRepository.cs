using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Brand?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Brand?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
