using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Production.Recipes.Commands.AddIngredient
{
    public class AddRecipeIngredientSpecification
        : BaseSpecification<Recipe>
    {
        public AddRecipeIngredientSpecification(AddRecipeIngredientCommand command)
        {
            Criteria = r => string.Equals(r.PublicId, command.Id);

            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude(ri => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude(ri => ri.Unit));
        }
    }
}
