using FluentValidation;
using TenderManagementSystem.Application.Commands.Bids;

namespace TenderManagementSystem.Application.Validators.Bids;

public class UpdateBidStatusCommandValidator : AbstractValidator<UpdateBidStatusCommand>
{
    public UpdateBidStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
    }
}