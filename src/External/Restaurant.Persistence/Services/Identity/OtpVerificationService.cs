using Microsoft.Extensions.Logging;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification;
using Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Email;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Persistence.Services.Identity
{
    internal class OtpVerificationService : IOtpVerificationService
    {
        private const int MaxFailedAttempts = 5;

        private readonly IUserRepository _userRepository;
        private readonly IOtpVerificationRepository _otpVerificationRepository;

        private readonly IOtpHasher _otpHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OtpVerificationService> _logger;

        private readonly IEmailService _emailService;

        public OtpVerificationService(
            IUserRepository userRepository,
            IOtpVerificationRepository otpVerificationRepository,
            IOtpHasher otpHasher,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ILogger<OtpVerificationService> logger)
        {
            _userRepository = userRepository;
            _otpVerificationRepository = otpVerificationRepository;
            _otpHasher = otpHasher;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task InitializeAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            var verificationCode = GenerateCode();
            var otpVerification = new OtpVerification(
                user.Id,
                _otpHasher.HashOtp(verificationCode),
                OtpPurpose.EmailVerification);
            _otpVerificationRepository.Add(otpVerification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            try
            {
                var message = new EmailMessage(user.UserName, verificationCode);
                await _emailService.SendEmailAsync(user.Email, message, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send verification email failed.");
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
            if (otpVerification is not null)
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

        private string GenerateCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
