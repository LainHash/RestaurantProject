using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetAllByProductId
{
    public class GetAllRecipesByProductIdSpecification
        : BaseSpecification<Recipe>
    {
        public GetAllRecipesByProductIdSpecification(GetAllRecipesByProductIdQuery query)
        {
            EnableSoftDeleteFilter();
            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));

            Criteria = r => string.Equals(r.Product.PublicId, query.ProductId);
        }
    }
}
