using FluentValidation;
using TenderManagementSystem.Application.Commands.Tenders;

namespace TenderManagementSystem.Application.Validators.Tenders;

public class CreateTenderCommandValidator : AbstractValidator<CreateTenderCommand>
{
    public CreateTenderCommandValidator()
    {
        RuleFor(x => x.Tender.Title).NotEmpty();
        RuleFor(x => x.Tender.Description).NotEmpty();
        RuleFor(x => x.Tender.Deadline).GreaterThan(DateTime.UtcNow);
        RuleFor(x => x.Tender.CategoryId).NotEmpty();
        RuleFor(x => x.Tender.StatusId).NotEmpty();
    }
}