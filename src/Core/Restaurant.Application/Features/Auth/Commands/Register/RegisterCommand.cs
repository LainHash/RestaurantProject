using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(RegisterRequest Body)
        : IRequest<Result>
    {
    }
}
