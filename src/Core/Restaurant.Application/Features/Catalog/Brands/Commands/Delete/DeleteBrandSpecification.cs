using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Delete
{
    public class DeleteBrandSpecification
        : BaseSpecification<Brand>
    {
        public DeleteBrandSpecification(DeleteBrandCommand command)
        {
            Criteria = brand => string.Equals(brand.PublicId, command.Id);
        }
    }
}
