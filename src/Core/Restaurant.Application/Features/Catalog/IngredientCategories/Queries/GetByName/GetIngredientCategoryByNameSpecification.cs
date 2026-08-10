using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetByName
{
    public class GetIngredientCategoryByNameSpecification
        : BaseSpecification<IngredientCategory>
    {
        public GetIngredientCategoryByNameSpecification(GetIngredientCategoryByNameQuery query)
        {
            Criteria = category => string.Equals(category.Name, query.Name);
            EnableSoftDeleteFilter();
        }
    }
}
