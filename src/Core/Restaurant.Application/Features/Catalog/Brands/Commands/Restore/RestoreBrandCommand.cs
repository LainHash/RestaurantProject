using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Restore
{
    public record RestoreBrandCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
