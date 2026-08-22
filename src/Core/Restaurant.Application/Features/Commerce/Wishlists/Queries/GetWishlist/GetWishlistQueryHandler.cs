using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist
{
    internal class GetWishlistQueryHandler(IWishlistService wishlistService)
                : IRequestHandler<GetWishlistQuery, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetWishlistSpecification(request);
            var response = await _wishlistService.GetAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
