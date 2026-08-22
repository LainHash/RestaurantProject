using MediatR;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Queries.GetAll
{
    public record GetAllIngredientsQuery(string? CategoryId, string? BrandId)
        : PageQuery, IRequest<PageResult<IEnumerable<IngredientResponse>>>
    {
    }
}
