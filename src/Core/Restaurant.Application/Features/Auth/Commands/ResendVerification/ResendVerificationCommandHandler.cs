using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.ResendVerification
{
    internal class ResendVerificationCommandHandler(IAuthenticationService authenticationService)
                : IRequestHandler<ResendVerificationCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(ResendVerificationCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ResendVerificationAsync(request, cancellationToken);
            return response;
        }
    }
}
