using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    public class RestoreProductSpecification
        : BaseSpecification<Product>
    {
        public RestoreProductSpecification(RestoreProductCommand command)
        {
            Criteria = p => string.Equals(p.PublicId, command.Id);
        }
    }
}
