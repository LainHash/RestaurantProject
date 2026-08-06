using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Domain.Repositories.Territory
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<Branch?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}
