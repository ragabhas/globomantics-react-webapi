using FluentValidation;

public class BidDtoValidator : AbstractValidator<BidDto>
{
    public BidDtoValidator()
    {
        RuleFor(x => x.Bidder).NotEmpty().WithMessage("The Bidder field is required");
        RuleFor(x => x.Amount).NotEmpty().WithMessage("The Amount field is required");
    }
}