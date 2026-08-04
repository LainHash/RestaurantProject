using MediatR;
using Restaurant.Application.Models.Results;
using Restaurant.Contract.DTOs.Catalog.Brands;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Create
{
    public record CreateBrandCommand(CreateBrandRequest Body)
        : IRequest<Result<BrandResponse>>
    {
    }
}
