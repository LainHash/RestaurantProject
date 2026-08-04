using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetById
{
    public record GetCategoryByIdQuery(string Id) 
        : IRequest<Result<CategoryResponse>>
    {
    }
}
