using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries;

public class GetAccountStatementQuery : IRequest<BaseResponse<GeneralAccountStatementResponse>>
{
    public string AccountNo { get; set; }
}
