using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    public class UpdateCategorySpecification
        : BaseSpecification<Category>
    {
        public UpdateCategorySpecification(UpdateCategoryCommand command)
        {
            Criteria = c => string.Equals(c.PublicId, command.Id);
        }
    }
}
