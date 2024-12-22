using AwesomeGICBank.ApplicationServices.Common.Parsers;
using AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries;
using AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;
using AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.ConsoleClient.Enums;
using AwesomeGICBank.ConsoleClient.Helpers;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.ConsoleClient;

public class BankClient
{
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;

    public BankClient(IMediator mediator, IServiceProvider serviceProvider)
    {
        _mediator = mediator;
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
                Console.WriteLine(ex.Message);
            }
        }
    }

    public async Task<bool> ExecuteOperationAsync(Operation operation)
    {
        using (var scope = _serviceProvider.CreateScope())
        using (var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>())
        {
            {
                var bankOpen = operation switch
                {
                    Operation.InputTransactions => await ProcessTransactionInputAsync(),
                    Operation.DefineInterestRules => await ProcessInterestRuleAsync(),
                    Operation.PrintStatement => await ProcessPrintStatementAsync(),
                    Operation.Quit => await ProcessQuitAsync(),
                    _ => await ProcessInvalidOperationAsync()
                };

                return bankOpen;
            }

        }
    }

    private async Task<bool> ProcessPrintStatementAsync()
    {
        Console.Write("Please enter account and month to generate the statement <Account> <Year><Month>\r\n(or enter blank to go back to main menu):\r\n>");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;
        BaseResponse<GeneralAccountStatementResponse> response1 = (await _mediator.Send(
            new GetAccountStatementQuery()
            {
                AccountNo = input
            }));
        Console.WriteLine(response1.ReturnValue.ToString());
        return true;
    }

    private async Task<bool> ProcessInterestRuleAsync()
    {
        Console.Write("\nPlease enter interest rules details in <Date> <RuleId> <Rate in %> format " +
            "\r\n(or enter blank to go back to main menu):" +
            "\r\n>");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;

        var interestRuleDto = InterestRuleDtoParser.Parse(input);
        var response = await _mediator.Send(
            new CreateInterestRuleCommand()
            {
                InterestRule = interestRuleDto
            });
        Console.WriteLine(response.ReturnValue.ToString());
        return true;
    }

    private async Task<bool> ProcessTransactionInputAsync()
    {
        Console.Write(
            "\nPlease enter transaction details in <Date> <Account> <Type> <Amount> format \n" +
            "(or enter blank to go back to main menu):" +
            "\r\n>");

        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return true;

        var transactionDto = TransactionDtoParser.Parse(input);
        var response = await _mediator.Send(
            new CreateAccountTransactionCommand
            {
                Transaction = transactionDto
            });
        Console.WriteLine(response.ToString());
        return true;
    }

    private async Task<bool> ProcessQuitAsync()
    {
        Console.WriteLine("\nThank you for banking with AwesomeGIC Bank." +
            "\r\nHave a nice day!");
        return false;
    }
    private static async Task<bool> ProcessInvalidOperationAsync()
    {
        Console.WriteLine("\nInvalid Operation. Please Select Correct Operation.\n");
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
