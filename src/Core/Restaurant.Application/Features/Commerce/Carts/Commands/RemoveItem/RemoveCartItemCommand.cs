using MediatR;
using Restaurant.Contract.DTOs.Commerce.CartItems;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem
{
    public record RemoveCartItemCommand(string? UserId, string? SessionId, RemoveCartItemRequest Body)
        : IRequest<Result<CartResponse>>
    {
    }
}
