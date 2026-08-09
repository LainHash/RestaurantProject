using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task<ProductCategory?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ProductCategory?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<ProductCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
