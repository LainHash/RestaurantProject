using AutoMapper;
using ConvenienceStore.Contract.DTOs.Authentication;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Auth.Commands.CompleteProfile;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Email;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Auth
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPersonalProfileRepository _personalProfileRepository;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IOtpVerificationService _otpVerificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider,
            IMapper mapper,
            ILogger<AuthenticationService> logger,
            ICustomerRepository customerRepository,
            IPersonalProfileRepository personalProfileRepository,
            IOtpVerificationService otpVerificationService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _logger = logger;
            _customerRepository = customerRepository;
            _personalProfileRepository = personalProfileRepository;
            _otpVerificationService = otpVerificationService;
        }

        public async Task<Result<AuthenticationResponse>> LoginAsync(
            LoginCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
            if (user is null || !_passwordHasher.VerifyPassword(command.Body.Password, user.PasswordHash))
            {
                return Result<AuthenticationResponse>
                    .Fail("Incorrect email or password.", HttpStatusCode.Unauthorized);
            }

            if (!user.IsActive)
            {
                return Result<AuthenticationResponse>
                    .Fail("Account is not active. Please verify your email.", HttpStatusCode.PreconditionRequired);
            }

            var role = await _roleRepository.FindByIdAsync(user.RoleId, cancellationToken);
            var roleName = role?.Name ?? "Customer";

            var token = _jwtProvider.GenerateToken(user.PublicId, user.UserName, user.Email, roleName);

            var response = new AuthenticationResponse(user, token);

            return Result<AuthenticationResponse>
                .Succeed(response, "Login successfully.");
        }

        public async Task<Result> RegisterAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingUser = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
                if (existingUser is not null)
                {
                    return Result<object>
                        .Fail("This email already used. Please use another email.", HttpStatusCode.Conflict);
                }

                var customerRole = await _roleRepository.FindByNameAsync("Customer", cancellationToken);
                if (customerRole is null)
                {
                    return Result<object>
                        .Fail(Error<Role>.NotFound, HttpStatusCode.InternalServerError);
                }

                var user = _mapper.Map<User>(command.Body)
                    .SetPasswordHash(_passwordHasher.HashPassword(command.Body.Password))
                    .SetRole(customerRole.Id);
                _userRepository.Add(user);

                await _otpVerificationService.InitializeAsync(user, cancellationToken);

                var customer = new Customer(user.Id);
                _customerRepository.Add(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result
                    .Succeed("Register successfully. Please check your account to get verification code.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Register request failed. Email: {Email}", command.Body.Email);
                return Result
                    .Fail("Register request failed.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> CompleteProfileAsync(
            CompleteProfileCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
            if (user is null)
            {
                return Result
                    .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            if (!user.IsActive)
            {
                return Result
                    .Fail("Account is not active. Please verify your email first.", HttpStatusCode.PreconditionRequired);
            }

            var personalProfile = await _personalProfileRepository.FindByUserAsync(user.Id, cancellationToken);
            if (personalProfile is not null)
            {
                return Result
                    .Fail("Profile has already been completed.", HttpStatusCode.Conflict);
            }

            personalProfile = _mapper.Map<PersonalProfile>(command.Body)
                .SetUser(user.Id);
            _personalProfileRepository.Add(personalProfile);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Profile completed successfully.", HttpStatusCode.Accepted);
        }
    }
}
