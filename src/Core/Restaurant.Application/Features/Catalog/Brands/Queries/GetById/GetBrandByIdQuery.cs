using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(string Id)
        : IRequest<Result<BrandResponse>>
    {
    }
}
