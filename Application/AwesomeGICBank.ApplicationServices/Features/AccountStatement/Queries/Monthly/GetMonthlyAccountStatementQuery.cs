using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;

public class GetMonthlyAccountStatementQuery : IRequest<BaseResponse<MonthlyAccountStatementResponse>>
{
    public StatementQueryDto StatementQuery { get; set; }
}
