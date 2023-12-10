using FluentValidation;

public class HouseDetailsValidator : AbstractValidator<HouseDetailsDto>
{
    public HouseDetailsValidator()
    {
        RuleFor(x => x.Address).NotEmpty().WithMessage("The Address field is required");
        RuleFor(x => x.Address).MaximumLength(50).WithMessage("A maximum of 50 characters is allowed for the Address field");
        RuleFor(x => x.Country).NotEmpty().WithMessage("The Country field is required");
    }
}