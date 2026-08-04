using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<BrandRecord, Brand>();

            CreateMap<Brand, BrandResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateBrandRequest, Brand>();

            CreateMap<UpdateBrandRequest, Brand>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember is string s)
                            return !string.IsNullOrWhiteSpace(s);

                        return srcMember != null;
                    }));
        }
    }
}
