using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Application.Features.Production.Recipes.Commands.Create
{
    public class CreateRecipeSpecification
        : BaseSpecification<Recipe>
    {
        public CreateRecipeSpecification(CreateRecipeCommand command)
        {
            AddInclude(x => x.Product);
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Ingredient));
            AddIncludeAggregator(x => x.Include(r => r.RecipeIngredients)
                                        .ThenInclude((RecipeIngredient ri) => ri.Unit));
        }

        public void ApplyCriteria(int id)
        {
            Criteria = r => r.Id == id;
        }
    }
}
