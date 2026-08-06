using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Products;
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

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ProductPrice.UnitPrice))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateProductRequest, Product>()
                .ForPath(dest => dest.ProductPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

            CreateMap<UpdateProductRequest, Product>()
                .ForPath(dest => dest.ProductPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        }
    }
}
