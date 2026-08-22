using MediatR;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.Merge
{
    public record MergeWishlistCommand(string UserId, string SessionId)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
