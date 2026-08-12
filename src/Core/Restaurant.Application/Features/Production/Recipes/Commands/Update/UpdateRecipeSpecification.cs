using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Application.Features.Production.Recipes.Commands.Update
{
    public class UpdateRecipeSpecification
        : BaseSpecification<Recipe>
    {
        public UpdateRecipeSpecification(UpdateRecipeCommand command)
        {
            Criteria = r => string.Equals(r.PublicId, command.Id);

            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }
    }
}
