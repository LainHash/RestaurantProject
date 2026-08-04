using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetByName
{
    public record GetCategoryByNameQuery(string Name)
         : IRequest<Result<CategoryResponse>>
    {
    }
}
