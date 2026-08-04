using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Category?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Category?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
