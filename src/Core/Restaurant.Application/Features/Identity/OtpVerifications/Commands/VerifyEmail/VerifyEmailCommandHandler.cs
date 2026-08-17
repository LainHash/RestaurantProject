using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.VerifyEmail
{
    internal class VerifyEmailCommandHandler(IOtpVerificationService otpVerificationService)
                : IRequestHandler<VerifyEmailCommand, Result>
    {
        private readonly IOtpVerificationService _otpVerificationService = otpVerificationService;

        public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _otpVerificationService.VerifyEmailAsync(request, cancellationToken);
            return response;
        }
    }
}
