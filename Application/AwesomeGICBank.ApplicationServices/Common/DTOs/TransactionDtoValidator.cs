using FluentValidation;

namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

internal class TransactionDtoValidator
    : AbstractValidator<TransactionDto>
{
    public TransactionDtoValidator()
    {
        RuleFor(p => p.TransactionDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(ClientInputValidators.IsDateOnly).WithMessage("Transaction: {PropertyName} format should be YYYYMMDD"); ;

        RuleFor(p => p.AccountNo)
            .NotEmpty().WithMessage("Transaction: {PropertyName} is required.");

        RuleFor(p => p.TransactionType)
            .NotEmpty().WithMessage("Transaction: {PropertyName} is required.")
            .Must(x => x.Length == 1).WithMessage("Transaction: {PropertyName} must be a character");

        RuleFor(p => p.Amount)
            .NotEmpty().WithMessage("Transaction: {PropertyName} is required.")
             .Must(ClientInputValidators.IsDecimal).WithMessage("Transaction: {PropertyName} should be a number");
    }
}

