using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Update
{
    public class UpdateIngredientSpecification
        : BaseSpecification<Ingredient>
    {
        public UpdateIngredientSpecification(UpdateIngredientCommand command)
        {
            Criteria = p => string.Equals(p.PublicId, command.Id);

            AddInclude(p => p.IngredientCategory);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.IngredientPrice);
            AddInclude(p => p.BaseUnit);
        }
    }
}
