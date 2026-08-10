using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class IngredientCategoryMapping : Profile
    {
        public IngredientCategoryMapping()
        {
            CreateMap<IngredientCategoryRecord, IngredientCategory>();

            CreateMap<IngredientCategory, IngredientCategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateIngredientCategoryRequest, IngredientCategory>();

            CreateMap<UpdateIngredientCategoryRequest, IngredientCategory>();
        }
    }
}
