using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetByName
{
    public record GetCategoryByNameQuery(string Name)
         : IRequest<Result<CategoryResponse>>
    {
    }
}
