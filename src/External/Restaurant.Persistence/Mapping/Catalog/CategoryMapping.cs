using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryRecord, Category>();

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));
        }
    }
}
