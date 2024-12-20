using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;

public class CreateAccountTransactionCommandHandler
    : IRequestHandler<CreateAccountTransactionCommand, BaseResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionDomainService _transactionDomainService;

    public CreateAccountTransactionCommandHandler(
        IAccountRepository accountRepository,
        ITransactionDomainService transactionDomainService)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _transactionDomainService = transactionDomainService ?? throw new ArgumentNullException(nameof(transactionDomainService));
    }

    public async Task<BaseResponse> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var validator = new TransactionDtoValidator();

        var validationResult = await validator.ValidateAsync(request.Transaction);

        if (validationResult.Errors.Count > 0)
        {
            return new BaseResponse(validationResult.Errors);
        }

        var transactionDto = request.Transaction;

        var transactionDate = DateTimeHelpers.ConvertDateStringToDateOnly(transactionDto.TransactionDate);
        var account = new Account(request.Transaction.AccountNo);
        var transactionId = _transactionDomainService.GenerateNextTransactionId(transactionDate);

        var transaction = new Transaction(
            transactionId,
            transactionType: EnumHelpers.ConvertToEnum<TransactionType>(transactionDto.TransactionType[0]),
            amount: AmountHelpers.ConvertToAmount(transactionDto.Amount),
            transactionDate: transactionDate);

        account.DoTransaction(transaction);

        await _accountRepository.CreateAsync(account);

        return new BaseResponse();
    }
}
