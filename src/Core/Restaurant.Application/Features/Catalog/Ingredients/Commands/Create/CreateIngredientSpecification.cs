using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Create
{
    public class CreateIngredientSpecification
        : BaseSpecification<Ingredient>
    {
        public CreateIngredientSpecification(CreateIngredientCommand command)
        {
            AddInclude(p => p.IngredientCategory);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.IngredientPrice);
            AddInclude(p => p.BaseUnit);
        }

        public void ApplyCriteria(int id)
        {
            Criteria = p => p.Id == id;
        }
    }
}
