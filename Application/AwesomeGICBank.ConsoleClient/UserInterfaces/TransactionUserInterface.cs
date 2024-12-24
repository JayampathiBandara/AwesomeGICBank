using AwesomeGICBank.ApplicationServices.Common.Enums;
using AwesomeGICBank.ApplicationServices.Common.Parsers;
using AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries;
using AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;
using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ConsoleClient.UserInterfaces;

public class TransactionUserInterface : BaseUserInterface
{
    public TransactionUserInterface(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Instruction = "\nPlease enter transaction details in <Date> <Account> <Type> <Amount> format \n" +
            "(or enter blank to go back to main menu):" +
            "\r\n>";
    }

    protected override async Task<bool> ProcessInput(string input, IMediator mediator)
    {
        var transactionDto = TransactionDtoParser.Parse(input);
        var response = await mediator.Send(
            new CreateAccountTransactionCommand
            {
                Transaction = transactionDto
            });
        if (response.ResponseType != ResponseTypes.Success)
        {
            Console.WriteLine(response.ToString());
            return true;
        }

        BaseResponse<GeneralAccountStatementResponse> result = await mediator.Send(
            new GetAccountStatementQuery()
            {
                AccountNo = transactionDto.AccountNo
            });

        result.PrintResponse();
        return true;
    }
}
