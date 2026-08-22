using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.Merge
{
    internal class MergeWishlistCommandHandler(IWishlistService wishlistService)
                : IRequestHandler<MergeWishlistCommand, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(MergeWishlistCommand request, CancellationToken cancellationToken)
        {
            var specification = new MergeWishlistSpecification(request);
            var response = await _wishlistService.MergeAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
