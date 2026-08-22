using MediatR;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.Merge
{
    public record MergeCartCommand(string UserId, string SessionId)
        : IRequest<Result<CartResponse>>
    {
    }
}
