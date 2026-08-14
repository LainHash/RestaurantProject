using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.RegisterCommand
{
    public class RegisterCommandHandler(IAuthService authService) 
        : IRequestHandler<RegisterCommand, Result<bool>>
    {
        private readonly IAuthService _authService = authService;

        public Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return _authService.RegisterAsync(request.Request, cancellationToken);
        }
    }
}
