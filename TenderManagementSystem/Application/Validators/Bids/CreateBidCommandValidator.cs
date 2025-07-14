using FluentValidation;
using TenderManagementSystem.Application.Commands.Bids;

namespace TenderManagementSystem.Application.Validators.Bids;

public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
{
    public CreateBidCommandValidator()
    {
        RuleFor(x => x.Bid.TenderId).NotEmpty();
        RuleFor(x => x.Bid.VendorId).NotEmpty();
        RuleFor(x => x.Bid.Amount).GreaterThan(0);
        RuleFor(x => x.Bid.Comments).MaximumLength(500);
    }
}