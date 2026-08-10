using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetById
{
    public class GetIngredientCategoryByIdSpecification
        : BaseSpecification<IngredientCategory>
    {
        public GetIngredientCategoryByIdSpecification(GetIngredientCategoryByIdQuery query)
        {
            Criteria = category => string.Equals(category.PublicId, query.Id);

            EnableSoftDeleteFilter();
        }
    }
}
