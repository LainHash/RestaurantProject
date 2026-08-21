using MediatR;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByUserId
{
    public record GetWishlistByUserIdQuery(string UserId)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
