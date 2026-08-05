using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Delete
{
    public class DeleteProductSpecification
        : BaseSpecification<Product>
    {
        public DeleteProductSpecification(DeleteProductCommand command)
        {
            Criteria = p => string.Equals(p.PublicId, command.Id);
        }
    }
}
