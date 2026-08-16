using FluentValidation;

namespace Restaurant.Application.Features.Auth.Commands.CompleteProfile
{
    public class CompleteProfileCommandValidator
        : AbstractValidator<CompleteProfileCommand>
    {
        public CompleteProfileCommandValidator()
        {
            RuleFor(x => x.Body.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name must not exceed 50 characters.");

            RuleFor(x => x.Body.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters.");

            RuleFor(x => x.Body.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.");

            RuleFor(x => x.Body.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(x => x.Body.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

            RuleFor(x => x.Body.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

            RuleFor(x => x.Body.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits.");

            RuleFor(x => x.Body.CitizenCardId)
                .NotEmpty().WithMessage("Citizen Card ID is required.")
                .MaximumLength(20).WithMessage("Citizen Card ID must not exceed 20 characters.");
        }
    }
}
