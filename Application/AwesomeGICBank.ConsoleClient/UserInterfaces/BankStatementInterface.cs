using AwesomeGICBank.ApplicationServices.Common.Parsers;
using AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;
using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ConsoleClient.UserInterfaces;

public class BankStatementInterface : BaseUserInterface
{
    public BankStatementInterface(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Instruction = "Please enter account and month to generate the statement <Account> <Year><Month>" +
            "\r\n(or enter blank to go back to main menu):\r\n>";
    }

    protected override async Task<bool> ProcessInput(string input, IMediator mediator)
    {
        var statementQuery = StatementQueryDtoParser.Parse(input);

        BaseResponse<MonthlyAccountStatementResponse> response = await mediator.Send(
            new GetMonthlyAccountStatementQuery()
            {
                StatementQuery = new() { AccountNo = statementQuery.AccountNo, YearMonth = statementQuery.YearMonth }
            });
        response.PrintResponse();
        return true;
    }
}
