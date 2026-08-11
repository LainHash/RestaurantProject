using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Repositories.Catalog
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Ingredient?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Ingredient?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
