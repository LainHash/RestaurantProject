using AutoMapper;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductRecord, Product>()
                .ForMember(dest => dest.InventoryType, opt => opt.MapFrom(src => Enum.Parse<InventoryType>(src.InventoryType)));
        }
    }
}
