using MediatR;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId
{
    public record GetWishlistBySessionIdQuery(string SessionId)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
