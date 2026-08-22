using MediatR;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.Merge
{
    internal class MergeCartCommandHandler(ICartService cartService)
                : IRequestHandler<MergeCartCommand, Result<CartResponse>>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<Result<CartResponse>> Handle(MergeCartCommand request, CancellationToken cancellationToken)
        {
            var specification = new MergeCartSpecification(request);
            var response = await _cartService.MergeAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
