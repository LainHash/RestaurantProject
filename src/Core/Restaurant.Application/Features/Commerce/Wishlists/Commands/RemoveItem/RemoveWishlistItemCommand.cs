using MediatR;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem
{
    public record RemoveWishlistItemCommand(string? UserId, string? SessionId, RemoveWishlistItemRequest Body)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
