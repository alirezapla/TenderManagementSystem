using FluentValidation;
using TenderManagementSystem.Application.Commands.Tenders;

namespace TenderManagementSystem.Application.Validators.Tenders;

public class DeleteTenderCommandValidator : AbstractValidator<DeleteTenderCommand>
{
    public DeleteTenderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}