using AutoMapper;
using Restaurant.Contract.DTOs.Guest.Wallets;
using Restaurant.Domain.Entities.Guest;

namespace Restaurant.Persistence.Mapping.Guest
{
    internal class WalletMapping : Profile
    {
        public WalletMapping()
        {
            CreateMap<Wallet, WalletResponse>();
        }
    }
}
