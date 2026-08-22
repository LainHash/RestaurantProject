using AutoMapper;
using Restaurant.Application.Features.Commerce.Carts.Queries.GetCart;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
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

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ICustomerRepository customerRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
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
    }
}
