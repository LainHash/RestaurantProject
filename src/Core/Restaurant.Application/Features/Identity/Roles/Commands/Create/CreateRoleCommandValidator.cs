using FluentValidation;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Create
{
    public class CreateRoleCommandValidator
        : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Body.Name)
                .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.")
                .NotEmpty().WithMessage("Role name must not null or empty.");

            RuleFor(x => x.Body.Description)
                .MaximumLength(50).WithMessage("Role description must not exceed 500 characters.");
        }
    }
}
