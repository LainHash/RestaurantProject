using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.CompleteProfile
{
    internal class CompleteProfileCommandHandler(IAuthenticationService authenticationService)
                : IRequestHandler<CompleteProfileCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.CompleteProfileAsync(request, cancellationToken);
            return response;
        }
    }
}
