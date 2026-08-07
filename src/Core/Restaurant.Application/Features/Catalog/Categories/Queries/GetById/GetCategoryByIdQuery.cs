using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetById
{
    public record GetCategoryByIdQuery(string Id) 
        : IRequest<Result<CategoryResponse>>
    {
    }
}
