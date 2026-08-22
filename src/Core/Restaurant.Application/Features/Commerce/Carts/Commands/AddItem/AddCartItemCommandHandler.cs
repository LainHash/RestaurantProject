using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.AddItem
{
    internal class AddCartItemCommandHandler(ICartService cartService)
                : IRequestHandler<AddCartItemCommand, Result<CartResponse>>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<Result<CartResponse>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var specification = new AddCartItemSpecification(request);
            var response = await _cartService.AddItemAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
