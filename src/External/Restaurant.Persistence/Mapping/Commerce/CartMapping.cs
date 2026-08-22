using AutoMapper;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Persistence.Mapping.Commerce
{
    internal class CartMapping : Profile
    {
        public CartMapping()
        {
            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
        }
    }
}
