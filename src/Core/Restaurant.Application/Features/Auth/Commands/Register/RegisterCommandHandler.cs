using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandHandler(IAuthenticationService authenticationService)
                : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.RegisterAsync(request, cancellationToken);
            return response;
        }
    }
}
