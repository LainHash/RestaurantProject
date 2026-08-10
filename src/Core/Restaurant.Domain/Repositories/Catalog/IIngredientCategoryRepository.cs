using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IIngredientCategoryRepository : IRepository<IngredientCategory>
    {
        Task<IngredientCategory?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IngredientCategory?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IngredientCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
