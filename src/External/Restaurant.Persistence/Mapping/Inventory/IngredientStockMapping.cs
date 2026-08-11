using AutoMapper;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Persistence.DataRecords.Inventory;

namespace Restaurant.Persistence.Mapping.Inventory
{
    internal class IngredientStockMapping : Profile
    {
        public IngredientStockMapping()
        {
            CreateMap<IngredientStockRecord, IngredientStock>();

            CreateMap<IngredientStock, IngredientStockResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.Branch.Code))
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name));
        }
    }
}
