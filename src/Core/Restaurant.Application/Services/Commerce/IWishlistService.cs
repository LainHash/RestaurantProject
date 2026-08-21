using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByUserId;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Commerce
{
    public interface IWishlistService
    {
        Task<Result<WishlistResponse>> GetByCustomerIdAsync(
            GetWishlistByCustomerIdQuery query,
            GetWishlistByCustomerIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<WishlistResponse>> GetBySessionIdAsync(
            GetWishlistBySessionIdQuery query,
            GetWishlistBySessionIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<WishlistResponse>> GetByUserIdAsync(
            GetWishlistByUserIdQuery query,
            GetWishlistByUserIdSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
