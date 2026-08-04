using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Update
{
    public class UpdateBrandSpecification
        : BaseSpecification<Brand>
    {
        public UpdateBrandSpecification(UpdateBrandCommand command)
        {
            Criteria = b => string.Equals(b.PublicId, command.Id);
        }
    }
}
