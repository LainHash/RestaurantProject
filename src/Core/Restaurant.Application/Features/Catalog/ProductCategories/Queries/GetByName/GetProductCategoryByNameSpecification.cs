using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetByName
{
    public class GetProductCategoryByNameSpecification
        : BaseSpecification<ProductCategory>
    {
        public GetProductCategoryByNameSpecification(GetProductCategoryByNameQuery query)
        {
            Criteria = category => string.Equals(category.Name, query.Name);
            EnableSoftDeleteFilter();
        }
    }
}
