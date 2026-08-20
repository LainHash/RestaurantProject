using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Application.Services.Guest
{
    public interface IWalletService
    {
        Task<Wallet> GetOrCreateAsync(
            int customerId,
            Func<Wallet> factory,
            CancellationToken cancellationToken = default);
    }
}
