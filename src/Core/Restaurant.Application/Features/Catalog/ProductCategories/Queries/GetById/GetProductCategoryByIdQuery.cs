using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetById
{
    public record GetProductCategoryByIdQuery(string Id) 
        : IRequest<Result<ProductCategoryResponse>>
    {
    }
}
