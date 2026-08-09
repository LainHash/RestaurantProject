using AutoMapper;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models.Results;
using Restaurant.Persistence.DataRecords.Storage;

namespace Restaurant.Persistence.Mapping.Storage
{
    internal class ImageMapping : Profile
    {
        public ImageMapping()
        {
            CreateMap<ImageRecord, Image>();

            CreateMap<Image, ImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.IsPrimary, opt => opt.MapFrom(src => src.ProductImage.IsPrimary))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.ProductImage.DisplayOrder));

            CreateMap<CloudinaryUploadResult, Image>();

            CreateMap<Image, UploadImageResponse>();
        }
    }
}
