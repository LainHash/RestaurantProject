using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Inventory.IngredientStocks.Queries.GetAllByIngredientId
{
    public class GetAllIngredientStocksByIngredientIdSpecification
        : BaseSpecification<IngredientStock>
    {
        public GetAllIngredientStocksByIngredientIdSpecification(GetAllIngredientStocksByIngredientIdQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Ingredient);
            AddInclude(x => x.Branch);

            Criteria = ps => string.Equals(ps.Ingredient.PublicId, query.IngredientId);
        }
    }
}
