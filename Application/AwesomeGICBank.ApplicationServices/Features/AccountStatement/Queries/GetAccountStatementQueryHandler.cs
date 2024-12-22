using AwesomeGICBank.ApplicationServices.Common.Enums;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries;

public class GetAccountStatementQueryHandler :
    IRequestHandler<GetAccountStatementQuery, BaseResponse<GeneralAccountStatementResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAccountStatementQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ??
            throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<GeneralAccountStatementResponse>> Handle(
        GetAccountStatementQuery request,
        CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.GetAsync(request.AccountNo);
        if (account == null)
        {
            return new BaseResponse<GeneralAccountStatementResponse>(ResponseTypes.ClientError, "Account not found");
        }

        var response = new GeneralAccountStatementResponse
        {
            AccountNo = account.AccountNo,
        };

        foreach (var transaction in account.Transactions.OrderBy(x => x.TransactionId))
        {
            response
                .AccountStatementRecords
                .Add(new GeneralAccountStatementRecord
                {
                    Date = transaction.Date,
                    TransactionId = transaction.Id.Value,
                    Type = transaction.Type,
                    Amount = transaction.Amount
                });
        }

        return new BaseResponse<GeneralAccountStatementResponse>(response);
    }
}