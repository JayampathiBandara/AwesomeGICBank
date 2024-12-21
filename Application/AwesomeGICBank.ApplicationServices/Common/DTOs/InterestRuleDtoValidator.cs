using FluentValidation;

namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

internal class InterestRuleDtoValidator
    : AbstractValidator<InterestRuleDto>
{
    public InterestRuleDtoValidator()
    {
        RuleFor(p => p.Date)
            .NotEmpty().WithMessage("InterestRule: {PropertyName} is required.")
            .Must(ClientInputValidators.IsDateOnly).WithMessage("InterestRule: {PropertyName} format should be YYYYMMDD");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("InterestRule: {PropertyName} is required.");

        RuleFor(p => p.Rate)
            .NotEmpty().WithMessage("InterestRule: {PropertyName} is required.")
            .Must(ClientInputValidators.IsDecimal).WithMessage("InterestRule: {PropertyName} should be a number");
    }
}

