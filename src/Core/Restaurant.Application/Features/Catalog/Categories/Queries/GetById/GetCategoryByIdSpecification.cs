using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetById
{
    public class GetCategoryByIdSpecification
        : BaseSpecification<Category>
    {
        public GetCategoryByIdSpecification(GetCategoryByIdQuery query)
        {
            Criteria = category => string.Equals(category.PublicId, query.Id);

            EnableSoftDeleteFilter();
        }
    }
}
