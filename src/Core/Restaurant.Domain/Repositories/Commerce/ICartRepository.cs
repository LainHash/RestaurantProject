using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Domain.Repositories.Commerce
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<Cart?> FindBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default);
    }
}
