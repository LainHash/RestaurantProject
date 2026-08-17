using AutoMapper;
using ConvenienceStore.Contract.DTOs.Authentication;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Auth.Commands.CompleteProfile;
using Restaurant.Application.Features.Auth.Commands.Login;
using Restaurant.Application.Features.Auth.Commands.Register;
using Restaurant.Application.Features.Auth.Commands.ResendVerification;
using Restaurant.Application.Features.Auth.Commands.VerifyEmail;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Email;
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
        private const int MaxFailedAttempts = 5;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IOtpVerificationRepository _otpVerificationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPersonalProfileRepository _personalProfileRepository;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IOtpHasher _otpHasher;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IJwtProvider jwtProvider,
            IMapper mapper,
            ILogger<AuthenticationService> logger,
            IOtpHasher otpHasher,
            IOtpVerificationRepository otpVerificationRepository,
            ICustomerRepository customerRepository,
            IPersonalProfileRepository personalProfileRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _logger = logger;
            _otpHasher = otpHasher;
            _otpVerificationRepository = otpVerificationRepository;
            _customerRepository = customerRepository;
            _personalProfileRepository = personalProfileRepository;
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

                var verificationCode = GenerateCode();
                var otpVerification = new OtpVerification(
                    user.Id,
                    _otpHasher.HashOtp(verificationCode),
                    OtpPurpose.EmailVerification);
                _otpVerificationRepository.Add(otpVerification);

                var customer = new Customer(user.Id);
                _customerRepository.Add(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                try
                {
                    var message = new EmailMessage(user.UserName, verificationCode);
                    await _emailService.SendEmailAsync(user.Email, message, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Send verification email failed.");
                }

                return Result
                    .Succeed("Register successfully. Please check your account to get verification code.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Register request failed. Email: {Email}", command.Body.Email);
                return Result<object>
                    .Fail("Register request failed.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> VerifyEmailAsync(
            VerifyEmailCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
            if (user is null)
            {
                return Result
                    .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            if (user.IsActive)
            {
                return Result
                    .Fail("Account is already active.", HttpStatusCode.Conflict);
            }

            var verification = await _otpVerificationRepository.FindActiveAsync(user.Id, OtpPurpose.EmailVerification, cancellationToken);

            if (verification is null)
            {
                return Result
                    .Fail("OTP verification not found", HttpStatusCode.NotFound);
            }

            if (verification.UsedAt is not null)
            {
                return Result
                    .Fail("OTP has already been used", HttpStatusCode.Conflict);
            }

            if (verification.ExpiresAt <= DateTime.UtcNow)
            {
                return Result
                    .Fail("OTP has expired");
            }

            if (verification.FailedAttempts >= MaxFailedAttempts)
            {
                return Result
                    .Fail("Too many failed attempts");
            }

            if (!_otpHasher.VerifyOtp(command.Body.Code, verification.CodeHash))
            {
                verification.IncrementFailedAttempt();
                return Result
                    .Fail("Invalid OTP");
            }

            verification.MarkAsUsed();

            user.CompleteVerification();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Email verified successfully. You can now login.");
        }

        public async Task<Result> ResendVerificationAsync(
            ResendVerificationCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByEmailAsync(command.Body.Email, cancellationToken);
            if (user is null)
            {
                return Result
                    .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
            }

            if (user.IsActive)
            {
                return Result
                    .Fail("Account is already active.", HttpStatusCode.Conflict);
            }

            var otpVerification = await _otpVerificationRepository.FindActiveAsync(user.Id, OtpPurpose.EmailVerification, cancellationToken);
            if( otpVerification is not null)
            {
                otpVerification.Invalidate();

                if (otpVerification.CreatedAt > DateTime.UtcNow.AddSeconds(60))
                {
                    return Result
                        .Fail("Please wait 60 senconds to resend verification.");
                }
            }

            var verificationCode = GenerateCode();
            var newOtpVerification = new OtpVerification(
                user.Id,
                _otpHasher.HashOtp(verificationCode),
                OtpPurpose.EmailVerification);
            _otpVerificationRepository.Add(newOtpVerification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var message = new EmailMessage(user.UserName, verificationCode);
            await _emailService.SendEmailAsync(user.Email, message, cancellationToken);

            return Result
                .Succeed("Verification email resent. Please check your inbox.");
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

        private string GenerateCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
