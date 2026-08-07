using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public record RestoreCategoryCommand(string Id)
        : IRequest<Result<object>>
    {
    }
}
