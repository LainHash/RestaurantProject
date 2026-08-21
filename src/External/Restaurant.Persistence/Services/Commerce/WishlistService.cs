using AutoMapper;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByUserId;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Commerce;
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
    internal class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistItemRepository _wishlistItemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WishlistService(
            IWishlistRepository wishlistRepository,
            IWishlistItemRepository wishlistItemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<WishlistResponse>> GetByCustomerIdAsync(
            GetWishlistByCustomerIdQuery query,
            GetWishlistByCustomerIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.FindByIdAsync(query.CustomerId, cancellationToken);
            if(customer is null)
            {
                return Result<WishlistResponse>
                    .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
            }

            var wishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if(wishlist is null)
            {
                wishlist = new Wishlist(customer.Id);
                _wishlistRepository.Add(wishlist);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var response = _mapper.Map<WishlistResponse>(wishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<Wishlist>.Retrieved);
        }

        public async Task<Result<WishlistResponse>> GetBySessionIdAsync(
            GetWishlistBySessionIdQuery query,
            GetWishlistBySessionIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var wishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (wishlist is null)
            {
                wishlist = new Wishlist(query.SessionId);
                _wishlistRepository.Add(wishlist);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var response = _mapper.Map<WishlistResponse>(wishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<Wishlist>.Retrieved);
        }

        public async Task<Result<WishlistResponse>> GetByUserIdAsync(
            GetWishlistByUserIdQuery query,
            GetWishlistByUserIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(query.UserId, cancellationToken);
            if(user is null)
            {
                return Result<WishlistResponse>
                    .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            var customer = await _customerRepository.FindByUserIdAsync(user.Id, cancellationToken);
            if (customer is null)
            {
                return Result<WishlistResponse>
                    .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
            }

            var wishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (wishlist is null)
            {
                wishlist = new Wishlist(customer.Id);
                _wishlistRepository.Add(wishlist);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var response = _mapper.Map<WishlistResponse>(wishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<Wishlist>.Retrieved);
        }
    }
}
