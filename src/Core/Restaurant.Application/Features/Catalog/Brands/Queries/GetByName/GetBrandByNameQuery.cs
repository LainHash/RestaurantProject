using MediatR;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetByName
{
    public record GetBrandByNameQuery(string Name)
         : IRequest<Result<BrandResponse>>
    {
    }
}
