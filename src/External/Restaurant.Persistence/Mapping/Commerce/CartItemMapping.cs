using AutoMapper;
using Restaurant.Contract.DTOs.Commerce.CartItems;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Persistence.Mapping.Commerce
{
    internal class CartItemMapping : Profile
    {
        public CartItemMapping()
        {
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
