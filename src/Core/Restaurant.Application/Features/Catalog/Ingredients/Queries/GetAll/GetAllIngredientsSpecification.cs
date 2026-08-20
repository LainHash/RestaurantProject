using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Ingredients.Queries.GetAll
{
    public class GetAllIngredientsSpecification
        : BaseSpecification<Ingredient>
    {
        public GetAllIngredientsSpecification(GetAllIngredientsQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(p => p.IngredientCategory);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.IngredientPrice);
            AddInclude(p => p.BaseUnit);



            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%"));
            }

            if (!string.IsNullOrWhiteSpace(query.CategoryId))
            {
                AddCriteria(p =>
                    p.IngredientCategory.PublicId == query.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(query.BrandId))
            {
                AddCriteria(p =>
                    p.Brand!.PublicId == query.BrandId);
            }

            switch (query.SortField)
            {
                case SortField.CreatedAt:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
                case SortField.Name:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.Name);
                    else
                        ApplyOrderByDescending(p => p.Name);
                    break;
                case SortField.Price:
                    if (query.Direction == SortDirection.Asc)
                        ApplyOrderBy(p => p.IngredientPrice!.UnitPrice);
                    else
                        ApplyOrderByDescending(p => p.IngredientPrice!.UnitPrice);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
