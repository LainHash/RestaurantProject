using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductSpecification
        : BaseSpecification<Product>
    {
        public CreateProductSpecification(CreateProductCommand command)
        {
            AddInclude(p => p.ProductCategory);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.ProductPrice);
        }

        public void ApplyCriteria(int id)
        {
            Criteria = p => p.Id == id;
        }
    }
}
