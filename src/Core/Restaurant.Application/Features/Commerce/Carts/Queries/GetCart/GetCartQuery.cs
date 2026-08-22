using MediatR;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Queries.GetCart
{
    public record GetCartQuery(string? UserId, string? SessionId)
        : IRequest<Result<CartResponse>>
    {
    }
}
