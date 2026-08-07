using MediatR;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Create
{
    public record CreateBrandCommand(CreateBrandRequest Body)
        : IRequest<Result<BrandResponse>>
    {
    }
}
