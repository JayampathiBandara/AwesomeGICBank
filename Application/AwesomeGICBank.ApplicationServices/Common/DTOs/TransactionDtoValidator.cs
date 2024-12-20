using FluentValidation;

namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

internal class TransactionDtoValidator
    : AbstractValidator<TransactionDto>
{
    public TransactionDtoValidator()
    {
        RuleFor(p => p.TransactionDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.AccountNo)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.TransactionType)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(x => x.Length == 1).WithMessage("{PropertyName} must be a character");

        RuleFor(p => p.Amount)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}

