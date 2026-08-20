using MediatR;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public record GetAllProductsQuery(string? CategoryId, string? BrandId) 
        : PageQuery, IRequest<PageResult<IEnumerable<ProductResponse>>>
    {
    }
}
