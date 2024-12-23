using AwesomeGICBank.ApplicationServices.Common.Enums;
using AwesomeGICBank.ApplicationServices.Common.Parsers;
using AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries;
using AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;
using AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;
using AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;
using AwesomeGICBank.ApplicationServices.Features.InterestRules.Queries;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.ConsoleClient.Enums;
using AwesomeGICBank.ConsoleClient.Helpers;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.ConsoleClient;

public class BankClient
{
    private readonly IServiceProvider _serviceProvider;

    public BankClient(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartBankAsync()
    {
        var bankOpen = true;
        while (bankOpen)
        {
            PrintWelcomeWindow();
            try
            {
                var operation = InputOperationReader.ReadOperation();
                bankOpen = await ExecuteOperationAsync(operation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex.Message}\n\n");
            }
        }
    }

    public async Task<bool> ExecuteOperationAsync(Operation operation)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        bool bankOpen = operation switch
        {
            Operation.InputTransactions => await ProcessTransactionInputAsync(mediator),
            Operation.DefineInterestRules => await ProcessInterestRuleAsync(mediator),
            Operation.PrintStatement => await ProcessPrintStatementAsync(mediator),
            Operation.Quit => ProcessQuit(),
            _ => ProcessInvalidOperation()
        };

        return bankOpen;

    }
    private async Task<bool> ProcessTransactionInputAsync(IMediator mediator)
    {
        Console.Write(
            "\nPlease enter transaction details in <Date> <Account> <Type> <Amount> format \n" +
            "(or enter blank to go back to main menu):" +
            "\r\n>");

        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;

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

    private async Task<bool> ProcessPrintStatementAsync(IMediator mediator)
    {
        Console.Write(
            "Please enter account and month to generate the statement <Account> <Year><Month>" +
            "\r\n(or enter blank to go back to main menu):\r\n>");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;

        var statementQuery = StatementQueryDtoParser.Parse(input);

        BaseResponse<MonthlyAccountStatementResponse> response = await mediator.Send(
            new GetMonthlyAccountStatementQuery()
            {
                StatementQuery = new() { AccountNo = statementQuery.AccountNo, YearMonth = statementQuery.YearMonth }
            });
        response.PrintResponse();
        return true;
    }

    private async Task<bool> ProcessInterestRuleAsync(IMediator mediator)
    {
        Console.Write("\nPlease enter interest rules details in <Date> <RuleId> <Rate in %> format " +
            "\r\n(or enter blank to go back to main menu):" +
            "\r\n>");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;

        var interestRuleDto = InterestRuleDtoParser.Parse(input);
        var response = await mediator.Send(
            new CreateInterestRuleCommand()
            {
                InterestRule = interestRuleDto
            });
        if (response.ResponseType != ResponseTypes.Success)
        {
            Console.WriteLine(response.ToString());
            return true;
        }

        BaseResponse<InterestRulesResponse>
            result = await mediator.Send(new GetInterestRulesQuery() { });

        result.PrintResponse();

        return true;
    }

    private static bool ProcessQuit()
    {
        Console.WriteLine("\n\nThank you for banking with AwesomeGIC Bank." +
            "\r\nHave a nice day!\n\n");
        return false;
    }
    private static bool ProcessInvalidOperation()
    {
        Console.WriteLine("\n\nInvalid Operation. Please Select Correct Operation.\n\n");
        return true;
    }

    private void PrintWelcomeWindow()
    {
        Console.Write(
            "Welcome to AwesomeGIC Bank! What would you like to do?\n" +
            "\t[T] Input transactions\n" +
            "\t[I] Define interest rules\n" +
            "\t[P] Print statement\n" +
            "\t[Q] Quit\n" +
            "> ");
    }
}
