using MediatR;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(string Id)
        : IRequest<Result<BrandResponse>>
    {
    }
}
