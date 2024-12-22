using FluentValidation;

namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

internal class StatementQueryDtoValidator
    : AbstractValidator<StatementQueryDto>
{
    public StatementQueryDtoValidator()
    {
        RuleFor(p => p.YearMonth)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(x => x.Length == 6).WithMessage("{PropertyName} Length should be 6")
            .Must(ClientInputValidators.IsYearMonthOnly).WithMessage("{PropertyName} format should be YYYYMM"); ;

        RuleFor(p => p.AccountNo)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}