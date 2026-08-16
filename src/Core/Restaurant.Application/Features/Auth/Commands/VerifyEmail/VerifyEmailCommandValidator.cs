using FluentValidation;

namespace Restaurant.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommandValidator
        : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Body.Code)
                .NotEmpty().WithMessage("Verification Code is required.")
                .Length(6).WithMessage("Verification Code must be exactly 6 digits.");
        }
    }
}
