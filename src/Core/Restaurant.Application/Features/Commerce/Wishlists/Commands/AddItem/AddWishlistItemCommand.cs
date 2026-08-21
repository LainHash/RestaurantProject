using MediatR;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem
{
    public record AddWishlistItemCommand(string? CustomerId, string? SessionId, AddWishlistItemRequest Body)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
