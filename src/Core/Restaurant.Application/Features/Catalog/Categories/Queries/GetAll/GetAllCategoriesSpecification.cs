using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll
{
    public class GetAllCategoriesSpecification
        : BaseSpecification<Category>
    {
        public GetAllCategoriesSpecification(GetAllCategoriesQuery query)
        {
            EnableSoftDeleteFilter();
        }
    }
}
