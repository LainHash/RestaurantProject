using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Guest;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Repositories.Guest;

namespace Restaurant.Persistence.Services.Guest
{
    internal class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(
            IWalletRepository walletRepository,
            IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        private async Task<Wallet> InitializeAsync(
            Func<Wallet> factory,
            CancellationToken cancellationToken = default)
        {
            var wallet = factory();

            _walletRepository.Add(wallet);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return wallet;
        }

        public async Task<Wallet> GetOrCreateAsync(
            int customerId,
            Func<Wallet> factory,
            CancellationToken cancellationToken = default)
        {
            var wallet = await _walletRepository.FindByCustomerIdAsync(customerId, cancellationToken);

            if(wallet is not null)
            {
                return wallet;
            }

            return await InitializeAsync(factory, cancellationToken);
        }
    }
}
