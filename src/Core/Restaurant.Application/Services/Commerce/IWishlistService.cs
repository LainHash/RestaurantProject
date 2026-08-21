using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId;
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
    }
}
