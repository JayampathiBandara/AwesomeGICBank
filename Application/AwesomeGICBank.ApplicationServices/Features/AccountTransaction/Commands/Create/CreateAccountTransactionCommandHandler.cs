using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;

public class CreateAccountTransactionCommandHandler
    : IRequestHandler<CreateAccountTransactionCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionDomainService _transactionDomainService;


    public CreateAccountTransactionCommandHandler(
        IUnitOfWork unitOfWork,
        ITransactionDomainService transactionDomainService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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

        var account = new Account(request.Transaction.AccountNo);
        var transaction = await _transactionDomainService.CreateTransactionAsync(
            transactionType: request.Transaction.TransactionType,
            amount: request.Transaction.Amount,
            transactionDate: request.Transaction.TransactionDate);


        await _unitOfWork.AccountRepository.CreateAsync(account);

        await _unitOfWork.TransactionRepository.CreateAsync(account.AccountNo, transaction);
        await _unitOfWork.SaveAsync();
        return new BaseResponse();
    }
}
