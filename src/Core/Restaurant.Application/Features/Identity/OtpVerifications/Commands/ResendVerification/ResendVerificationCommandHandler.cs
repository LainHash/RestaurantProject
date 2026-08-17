using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ResendVerification
{
    internal class ResendVerificationCommandHandler(IOtpVerificationService otpVerificationService)
                : IRequestHandler<ResendVerificationCommand, Result>
    {
        private readonly IOtpVerificationService _otpVerificationService = otpVerificationService;

        public async Task<Result> Handle(ResendVerificationCommand request, CancellationToken cancellationToken)
        {
            var response = await _otpVerificationService.ResendVerificationAsync(request, cancellationToken);
            return response;
        }
    }
}
