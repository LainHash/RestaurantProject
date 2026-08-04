using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Update
{
    public record UpdateBrandCommand(string Id, UpdateBrandRequest Body)
        : IRequest<Result<BrandResponse>>
    {
    }
}
