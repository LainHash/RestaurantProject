using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class ProductCategoryMapping : Profile
    {
        public ProductCategoryMapping()
        {
            CreateMap<ProductCategoryRecord, ProductCategory>();

            CreateMap<ProductCategory, ProductCategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateProductCategoryRequest, ProductCategory>();

            CreateMap<UpdateProductCategoryRequest, ProductCategory>();
        }
    }
}
