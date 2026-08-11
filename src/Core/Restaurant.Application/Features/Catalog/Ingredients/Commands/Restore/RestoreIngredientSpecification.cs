using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Restore
{
    public class RestoreIngredientSpecification
        : BaseSpecification<Ingredient>
    {
        public RestoreIngredientSpecification(RestoreIngredientCommand command)
        {
            Criteria = p => string.Equals(p.PublicId, command.Id);
        }
    }
}
