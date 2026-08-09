using MediatR;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetByName
{
    public record GetProductCategoryByNameQuery(string Name)
         : IRequest<Result<ProductCategoryResponse>>
    {
    }
}
