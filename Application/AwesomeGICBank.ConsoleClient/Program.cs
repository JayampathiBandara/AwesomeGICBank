using AwesomeGICBank.ApplicationServices;
using AwesomeGICBank.ApplicationServices.Common.Parsers;
using AwesomeGICBank.ApplicationServices.Features.AccountTransaction.Commands.Create;
using AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.ConsoleClient.Enums;
using AwesomeGICBank.ConsoleClient.Helpers;
using AwesomeGICBank.DomainServices;
using AwesomeGICBank.SqlServerPersistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.ConsoleClient;

public class Program
{
    private static IMediator _mediator;

    static async Task Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        services.AddDomainServices();

        services.AddPersistenceServices();

        services.AddApplicationServices();

        // Build service provider
        var serviceProvider = services.BuildServiceProvider();

        // Get mediator instance
        _mediator = serviceProvider.GetRequiredService<IMediator>();

        var bankOpen = true;
        while (bankOpen)
        {
            Console.Write(
                "Welcome to AwesomeGIC Bank! What would you like to do?\n" +
                "\t[T] Input transactions\n" +
                "\t[I] Define interest rules\n" +
                "\t[P] Print statement\n" +
                "\t[Q] Quit\n" +
                "> ");

            try
            {
                var operation = InputOperationReader.ReadOperation();

                string input;
                BaseResponse response;
                switch (operation)
                {
                    case Operation.InputTransactions:
                        Console.WriteLine("\nPlease enter transaction details in <Date> <Account> <Type> <Amount> format \n(or enter blank to go back to main menu):");
                        input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input))
                            break;
                        var transactionDto = TransactionDtoParser.Parse(input);
                        response = await _mediator.Send(
                            new CreateAccountTransactionCommand()
                            {
                                Transaction = transactionDto
                            });

                        Console.WriteLine(response.ToString());

                        break;
                    case Operation.DefineInterestRules:
                        Console.Write("\nPlease enter interest rules details in <Date> <RuleId> <Rate in %> format \r\n(or enter blank to go back to main menu):\r\n>");
                        input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input))
                            break;
                        var interestRuleDto = InterestRuleDtoParser.Parse(input);
                        response = await _mediator.Send(
                            new CreateInterestRuleCommand()
                            {
                                InterestRule = interestRuleDto
                            });
                        Console.WriteLine(response.ToString());
                        break;
                    case Operation.PrintStaqtement:
                        Console.WriteLine(operation);
                        break;
                    case Operation.Quit:
                        Console.WriteLine("\nThank you for banking with AwesomeGIC Bank.\r\nHave a nice day!");
                        bankOpen = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid Operation. Please Select Correct Operaion.\n");
                        break;

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
