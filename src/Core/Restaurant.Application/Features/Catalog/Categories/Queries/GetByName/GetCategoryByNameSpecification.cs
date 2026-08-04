using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetByName
{
    public class GetCategoryByNameSpecification
        : BaseSpecification<Category>
    {
        public GetCategoryByNameSpecification(GetCategoryByNameQuery query)
        {
            Criteria = category => string.Equals(category.Name, query.Name);
            EnableSoftDeleteFilter();
        }
    }
}
