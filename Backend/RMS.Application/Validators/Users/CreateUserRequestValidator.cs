using FluentValidation;
using RMS.Application.Models.Users;

namespace RMS.Application.Validators.Users;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.RestaurantId)
            .GreaterThan(0)
            .WithMessage("Restaurant is required.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage("Role is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithMessage("First name cannot be longer than 50 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .WithMessage("Last name cannot be longer than 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.")
            .MaximumLength(255)
            .WithMessage("Email cannot be longer than 255 characters.");

        RuleFor(x => x.MobileNumber)
            .MaximumLength(15)
            .WithMessage("Mobile number cannot be longer than 15 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.MobileNumber));

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100)
            .WithMessage("Password cannot be longer than 100 characters.");
    }
}