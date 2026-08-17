using FluentValidation;

namespace Restaurant.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator
        : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Body.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Body.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
