using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Restore
{
    public class RestoreIngredientCategorySpecification
        : BaseSpecification<IngredientCategory>
    {
        public RestoreIngredientCategorySpecification(RestoreIngredientCategoryCommand command)
        {
            Criteria = category => string.Equals(category.PublicId, command.Id);
        }
    }
}
