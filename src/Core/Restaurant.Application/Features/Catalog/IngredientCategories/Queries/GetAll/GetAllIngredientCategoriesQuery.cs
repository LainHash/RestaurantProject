using MediatR;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.IngredientCategories.Queries.GetAll
{
    public record GetAllIngredientCategoriesQuery
        : PageQuery, IRequest<PageResult<IEnumerable<IngredientCategoryResponse>>>
    {
    }
}
