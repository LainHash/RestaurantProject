using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Domain.Repositories.Guest
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<Wallet?> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    }
}
