using AutoMapper;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Persistence.Mapping.Commerce
{
    internal class WishlistItemMapping : Profile
    {
        public WishlistItemMapping()
        {
            CreateMap<WishlistItem, WishlistItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
