using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Common.Enums;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;

public class GetMonthlyAccountStatementQueryHandler :
    IRequestHandler<GetMonthlyAccountStatementQuery, BaseResponse<MonthlyAccountStatementResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMonthlyAccountStatementQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ??
            throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<MonthlyAccountStatementResponse>> Handle(
        GetMonthlyAccountStatementQuery request,
        CancellationToken cancellationToken)
    {
        var validator = new StatementQueryDtoValidator();

        var validationResult = await validator.ValidateAsync(request.StatementQuery);

        if (validationResult.Errors.Count > 0)
        {
            return new BaseResponse<MonthlyAccountStatementResponse>(validationResult.Errors);
        }

        var account = await _unitOfWork
            .AccountRepository
            .GetAsync(request.StatementQuery.AccountNo, request.StatementQuery.Year, request.StatementQuery.Month);

        if (account == null)
        {
            return new BaseResponse<MonthlyAccountStatementResponse>(ResponseTypes.ClientError, "Account not found");
        }

        var response = new MonthlyAccountStatementResponse
        {
            AccountNo = account.AccountNo,
        };

        foreach (var transaction in account.Transactions.OrderBy(x => x.TransactionId))
        {
            response
                .MonthlyAccountStatementRecords
                .Add(new MonthlyAccountStatementRecord
                {
                    Date = transaction.Date,
                    TransactionId = transaction.Id.Value,
                    Type = transaction.Type,
                    Amount = transaction.Amount
                });
        }

        return new BaseResponse<MonthlyAccountStatementResponse>(response);
    }
}