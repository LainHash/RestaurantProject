using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetByName
{
    public record GetBrandByNameQuery(string Name)
         : IRequest<Result<BrandResponse>>
    {
    }
}
