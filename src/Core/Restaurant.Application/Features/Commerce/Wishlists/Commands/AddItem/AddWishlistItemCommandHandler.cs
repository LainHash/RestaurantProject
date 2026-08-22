using MediatR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem
{
    internal class AddWishlistItemCommandHandler(IWishlistService wishlistService)
                : IRequestHandler<AddWishlistItemCommand, Result<WishlistResponse>>
    {
        private readonly IWishlistService _wishlistService = wishlistService;

        public async Task<Result<WishlistResponse>> Handle(AddWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var specification = new AddWishlistItemSpecification(request);
            var response = await _wishlistService.AddItemAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
