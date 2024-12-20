using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;

public class CreateAccountTransactionCommand : IRequest<BaseResponse>
{
    public TransactionDto Transaction { get; set; }
}
