using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Queries.GetCart
{
    internal class GetCartQueryHandler(ICartService cartService)
                : IRequestHandler<GetCartQuery, Result<CartResponse>>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<Result<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCartSpecification(request);
            var response = await _cartService.GetAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
