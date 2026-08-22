using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem
{
    internal class RemoveCartItemCommandHandler(ICartService cartService)
                : IRequestHandler<RemoveCartItemCommand, Result<CartResponse>>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<Result<CartResponse>> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var specification = new RemoveCartItemSpecification(request);
            var response = await _cartService.RemoveItemAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
