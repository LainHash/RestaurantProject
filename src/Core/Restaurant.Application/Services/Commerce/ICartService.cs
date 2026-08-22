using Restaurant.Application.Features.Commerce.Carts.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Carts.Queries.GetCart;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Commerce
{
    public interface ICartService
    {
        Task<Result<CartResponse>> GetAsync(
            GetCartQuery query,
            GetCartSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<CartResponse>> AddItemAsync(
            AddCartItemCommand command,
            AddCartItemSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<CartResponse>> RemoveItemAsync(
            RemoveCartItemCommand command,
            RemoveCartItemSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
