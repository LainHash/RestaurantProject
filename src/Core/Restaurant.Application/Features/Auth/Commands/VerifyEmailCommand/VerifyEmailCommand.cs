using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.VerifyEmailCommand
{
    public record VerifyEmailCommand(VerifyEmailRequest Request) 
        : IRequest<Result<bool>>;
}
