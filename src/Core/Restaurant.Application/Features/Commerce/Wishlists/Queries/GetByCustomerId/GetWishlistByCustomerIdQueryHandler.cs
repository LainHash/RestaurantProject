using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId
{
    internal class GetWishlistByCustomerIdQueryHandler(IWishlistService wishlistService)
                : IRequestHandler<GetWishlistByCustomerIdQuery, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(GetWishlistByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetWishlistByCustomerIdSpecification(request);
            var response = await _wishlistService.GetByCustomerIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
