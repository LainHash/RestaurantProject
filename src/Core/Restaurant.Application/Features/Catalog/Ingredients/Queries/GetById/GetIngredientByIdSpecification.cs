using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Ingredients.Queries.GetById
{
    public class GetIngredientByIdSpecification
        : BaseSpecification<Ingredient>
    {
        public GetIngredientByIdSpecification(GetIngredientByIdQuery query)
        {
            EnableSoftDeleteFilter();

            Criteria = p => string.Equals(p.PublicId, query.Id);

            AddInclude(p => p.IngredientCategory);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.IngredientPrice);
            AddInclude(p => p.BaseUnit);
        }
    }
}
