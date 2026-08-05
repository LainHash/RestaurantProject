using MediatR;
using Restaurant.Application.Models;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public record GetAllProductsQuery() 
        : PageQuery, IRequest<PageResult<IEnumerable<ProductResponse>>>
    {
    }
}
