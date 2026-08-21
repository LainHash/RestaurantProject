using MediatR;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId
{
    public record GetWishlistByCustomerIdQuery(string CustomerId)
        : IRequest<Result<WishlistResponse>>
    {
    }
}
