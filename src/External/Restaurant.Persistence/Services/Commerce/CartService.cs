using AutoMapper;
using Restaurant.Application.Features.Commerce.Carts.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Carts.Queries.GetCart;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Commerce
{
    internal class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<CartResponse>> GetAsync(
            GetCartQuery query,
            GetCartSpecification specification,
            CancellationToken cancellationToken = default)
        {
            Cart? cart;

            if (query.UserId != null)
            {
                var user = await _userRepository.FindByIdAsync(query.UserId, cancellationToken);
                if (user is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
                }

                var customer = await _customerRepository.FindByUserIdAsync(user.Id, cancellationToken);
                if (customer is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
                }

                cart = await _cartRepository.FindAsync(specification, cancellationToken);
                if (cart is null)
                {
                    cart = new Cart(customer.Id);
                    _cartRepository.Add(cart);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                cart = await _cartRepository.FindAsync(specification, cancellationToken);
                if (cart is null)
                {
                    cart = new Cart(query.SessionId!);
                    _cartRepository.Add(cart);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            var response = _mapper.Map<CartResponse>(cart);
            return Result<CartResponse>
                .Succeed(response, Success<Cart>.Retrieved);
        }

        public async Task<Result<CartResponse>> AddItemAsync(
            AddCartItemCommand command,
            AddCartItemSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<CartResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Cart? cart;

            if (command.UserId != null)
            {
                var user = await _userRepository.FindByIdAsync(command.UserId, cancellationToken);
                if (user is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
                }

                var customer = await _customerRepository.FindByUserIdAsync(user.Id, cancellationToken);
                if (customer is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
                }

                cart = await _cartRepository.FindAsync(specification, cancellationToken);
                if (cart is null)
                {
                    cart = new Cart(customer.Id);
                    _cartRepository.Add(cart);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                cart = await _cartRepository.FindAsync(specification, cancellationToken);
                if (cart is null)
                {
                    cart = new Cart(command.SessionId!);
                    _cartRepository.Add(cart);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (cartItem is null)
            {
                cartItem = new CartItem(cart.Id, product.Id);
                _cartItemRepository.Add(cartItem);
            }
            else
            {
                cartItem.UpdateQuantity();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CartResponse>(cart);
            return Result<CartResponse>
                .Succeed(response, Success<CartItem>.Added);
        }

        public async Task<Result<CartResponse>> RemoveItemAsync(
            RemoveCartItemCommand command,
            RemoveCartItemSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<CartResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Cart? cart;

            if (command.UserId != null)
            {
                var user = await _userRepository.FindByIdAsync(command.UserId, cancellationToken);
                if (user is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
                }

                var customer = await _customerRepository.FindByUserIdAsync(user.Id, cancellationToken);
                if (customer is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
                }

                cart = await _cartRepository.FindAsync(specification, cancellationToken);
                if (cart is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<Cart>.NotFound, HttpStatusCode.NotFound);
                }
            }
            else
            {
                cart = await _cartRepository.FindAsync(specification, cancellationToken);
                if (cart is null)
                {
                    return Result<CartResponse>
                        .Fail(Error<Cart>.NotFound, HttpStatusCode.NotFound);
                }
            }

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (cartItem is null)
            {
                return Result<CartResponse>
                    .Fail("This product has not been added to your cart.", HttpStatusCode.NotFound);
            }

            _cartItemRepository.Remove(cartItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CartResponse>(cart);
            return Result<CartResponse>
                .Succeed(response, Success<CartItem>.Deleted);
        }
    }
}
