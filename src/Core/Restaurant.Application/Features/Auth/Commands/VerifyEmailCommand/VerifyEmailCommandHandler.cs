using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.VerifyEmailCommand
{
    public class VerifyEmailCommandHandler(IAuthService authService) 
        : IRequestHandler<VerifyEmailCommand, Result<bool>>
    {
        private readonly IAuthService _authService = authService;

        public Task<Result<bool>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            return _authService.VerifyEmailAsync(request.Request, cancellationToken);
        }
    }
}
