using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByUserId
{
    internal class GetWishlistByUserIdQueryHandler(IWishlistService wishlistService)
                : IRequestHandler<GetWishlistByUserIdQuery, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(GetWishlistByUserIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetWishlistByUserIdSpecification(request);
            var response = await _wishlistService.GetByUserIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
