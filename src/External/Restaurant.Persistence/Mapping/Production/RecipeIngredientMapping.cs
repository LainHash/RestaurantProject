using AutoMapper;
using Restaurant.Contract.DTOs.Production.RecipeIngredients;
using Restaurant.Domain.Entities.Production;
using Restaurant.Persistence.DataRecords.Production;

namespace Restaurant.Persistence.Mapping.Production
{
    internal class RecipeIngredientMapping : Profile
    {
        public RecipeIngredientMapping()
        {
            CreateMap<RecipeIngredientRecord, RecipeIngredient>();

            CreateMap<RecipeIngredient, RecipeIngredientResponse>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.Symbol));
        }
    }
}
