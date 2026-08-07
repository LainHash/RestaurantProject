using MediatR;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetAll
{
    public record GetAllBrandsQuery
        : PageQuery, IRequest<PageResult<IEnumerable<BrandResponse>>>
    {
    }
}
