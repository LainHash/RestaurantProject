using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Delete
{
    public class DeleteProductCategorySpecification
        : BaseSpecification<ProductCategory>
    {
        public DeleteProductCategorySpecification(DeleteProductCategoryCommand command)
        {
            Criteria = category => string.Equals(category.PublicId, command.Id);
        }
    }
}
