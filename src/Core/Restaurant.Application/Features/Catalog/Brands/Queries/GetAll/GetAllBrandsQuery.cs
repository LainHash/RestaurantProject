using MediatR;
using Restaurant.Application.Models;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetAll
{
    public record GetAllBrandsQuery
        : PageQuery, IRequest<PageResult<IEnumerable<BrandResponse>>>
    {
    }
}
