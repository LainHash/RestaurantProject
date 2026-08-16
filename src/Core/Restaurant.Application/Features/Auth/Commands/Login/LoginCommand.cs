using ConvenienceStore.Contract.DTOs.Authentication;
using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(LoginRequest Body)
        : IRequest<Result<AuthenticationResponse>>
    {
    }
}
