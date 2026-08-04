using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public class RestoreCategorySpecification
        : BaseSpecification<Category>
    {
        public RestoreCategorySpecification(RestoreCategoryCommand command)
        {
            Criteria = category => string.Equals(category.PublicId, command.Id);
        }
    }
}
