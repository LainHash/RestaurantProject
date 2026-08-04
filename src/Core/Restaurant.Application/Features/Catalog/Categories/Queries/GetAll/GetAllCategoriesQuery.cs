using MediatR;
using Restaurant.Application.Models;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll
{
    public record GetAllCategoriesQuery
        : PageQuery, IRequest<PageResult<IEnumerable<CategoryResponse>>>
    {
    }
}
