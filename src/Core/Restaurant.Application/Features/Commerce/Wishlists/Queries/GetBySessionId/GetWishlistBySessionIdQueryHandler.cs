using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId
{
    internal class GetWishlistBySessionIdQueryHandler(IWishlistService wishlistService)
                : IRequestHandler<GetWishlistBySessionIdQuery, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(GetWishlistBySessionIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetWishlistBySessionIdSpecification(request);
            var response = await _wishlistService.GetBySessionIdAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
