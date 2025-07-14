using FluentValidation;
using TenderManagementSystem.Application.Commands.Vendors;

namespace TenderManagementSystem.Application.Validators.Vendors;

public class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
{
    public CreateVendorCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.UserId).NotEmpty();
    }
}