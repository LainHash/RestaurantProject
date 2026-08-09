using AutoMapper;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Persistence.DataRecords.Storage;

namespace Restaurant.Persistence.Mapping.Storage
{
    internal class ProductImageMapping : Profile
    {
        public ProductImageMapping()
        {
            CreateMap<ProductImageRecord, ProductImage>();

            CreateMap<ProductImage, ImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Image.PublicId))
                .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.Image.AltText))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Image.Url));
        }
    }
}
