using AutoMapper;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Persistence.DataRecords.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    internal class IngredientCategoryMapping : Profile
    {
        public IngredientCategoryMapping()
        {
            CreateMap<IngredientCategoryRecord, IngredientCategory>();
        }
    }
}
