using FluentValidation;
using RMS.Application.Models.MenuItems;

namespace RMS.Application.Validators.MenuItems;

public class UpdateMenuItemRequestValidator : AbstractValidator<UpdateMenuItemRequest>
{
    public UpdateMenuItemRequestValidator()
    {
        RuleFor(x => x.MenuItemId)
            .GreaterThan(0).WithMessage("MenuItemId is required.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId is required.");

        RuleFor(x => x.ItemName)
            .NotEmpty().WithMessage("ItemName is required.")
            .MaximumLength(150).WithMessage("ItemName must not exceed 150 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
