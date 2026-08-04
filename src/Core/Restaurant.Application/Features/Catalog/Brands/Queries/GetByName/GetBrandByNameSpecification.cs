using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetByName
{
    public class GetBrandByNameSpecification
        : BaseSpecification<Brand>
    {
        public GetBrandByNameSpecification(GetBrandByNameQuery query)
        {
            Criteria = brand => string.Equals(brand.Name, query.Name);
            EnableSoftDeleteFilter();
        }
    }
}
