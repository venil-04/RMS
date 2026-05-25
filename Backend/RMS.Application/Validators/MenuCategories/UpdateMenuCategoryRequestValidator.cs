using FluentValidation;
using RMS.Application.Models.MenuCategories;

namespace RMS.Application.Validators.MenuCategories;

public class UpdateMenuCategoryRequestValidator : AbstractValidator<UpdateMenuCategoryRequest>
{
    public UpdateMenuCategoryRequestValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId is required.");

        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("CategoryName is required.")
            .MaximumLength(100).WithMessage("CategoryName must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
