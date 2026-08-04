using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetById
{
    public class GetBrandByIdSpecification
        : BaseSpecification<Brand>
    {
        public GetBrandByIdSpecification(GetBrandByIdQuery query)
        {
            Criteria = brand => string.Equals(brand.PublicId, query.Id);

            EnableSoftDeleteFilter();
        }
    }
}
