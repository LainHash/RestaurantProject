using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem
{
    internal class RemoveWishlistItemCommandHandler(IWishlistService wishlistService)
                : IRequestHandler<RemoveWishlistItemCommand, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(RemoveWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var specification = new RemoveWishlistItemSpecification(request);
            var response = await _wishlistService.RemoveItemAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
