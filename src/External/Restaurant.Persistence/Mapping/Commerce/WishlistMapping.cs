using AutoMapper;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Persistence.Mapping.Commerce
{
    internal class WishlistMapping : Profile
    {
        public WishlistMapping()
        {
            CreateMap<Wishlist, WishlistResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.WishlistItems, opt => opt.MapFrom(src => src.WishlistItems));
        }
    }
}
