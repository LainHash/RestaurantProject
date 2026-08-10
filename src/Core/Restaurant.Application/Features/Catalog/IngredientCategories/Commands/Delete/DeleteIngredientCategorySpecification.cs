using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Commands.Delete
{
    public class DeleteIngredientCategorySpecification
        : BaseSpecification<IngredientCategory>
    {
        public DeleteIngredientCategorySpecification(DeleteIngredientCategoryCommand command)
        {
            Criteria = category => string.Equals(category.PublicId, command.Id);
        }
    }
}
