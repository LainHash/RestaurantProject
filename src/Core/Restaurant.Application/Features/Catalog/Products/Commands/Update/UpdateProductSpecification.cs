using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductSpecification
        : BaseSpecification<Product>
    {
        public UpdateProductSpecification(UpdateProductCommand command)
        {
            Criteria = p => string.Equals(p.PublicId, command.Id);

            AddInclude(p => p.Category);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.ProductPrice);
        }
    }
}
