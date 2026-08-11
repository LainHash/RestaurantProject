using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Delete
{
    public class DeleteIngredientSpecification
        : BaseSpecification<Ingredient>
    {
        public DeleteIngredientSpecification(DeleteIngredientCommand command)
        {
            Criteria = p => string.Equals(p.PublicId, command.Id);
        }
    }
}
