using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Production.Recipes.Queries.GetById
{
    public class GetRecipeByIdSpecification
        : BaseSpecification<Recipe>
    {
        public GetRecipeByIdSpecification(GetRecipeByIdQuery query)
        {
            EnableSoftDeleteFilter();

            Criteria = r => string.Equals(r.PublicId, query.Id);

            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }
    }
}
