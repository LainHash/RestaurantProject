using FluentValidation;

namespace Restaurant.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandValidator
        : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Body.UserName)
            .NotEmpty().WithMessage("UserName is required.")
            .MinimumLength(3).WithMessage("UserName must be at least 3 characters.");

            RuleFor(x => x.Body.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Body.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.Body.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Body.Password).WithMessage("Confirm Password must match the Password.");
        }
    }
}
