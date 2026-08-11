using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class IngredientMapping : Profile
    {
        public IngredientMapping()
        {
            CreateMap<IngredientRecord, Ingredient>();

            CreateMap<Ingredient, IngredientResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.IngredientPrice.UnitPrice))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.IngredientCategory.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.BaseUnit.Symbol));

            CreateMap<CreateIngredientRequest, Ingredient>()
                .ForPath(dest => dest.IngredientPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

            CreateMap<UpdateIngredientRequest, Ingredient>()
                .ForPath(dest => dest.IngredientPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        }
    }
}
