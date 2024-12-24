using AwesomeGICBank.ConsoleClient.Enums;
using AwesomeGICBank.ConsoleClient.Helpers;
using AwesomeGICBank.ConsoleClient.UserInterfaces;

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

                var userInterface = GetUserInterface(operation);

                bankOpen = await userInterface.DisplayAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n{ex.Message}\n\n");
            }
        }
    }

    private IUserInterface GetUserInterface(Operation operation)
    {
        IUserInterface userInterface = operation switch
        {
            Operation.InputTransactions => new TransactionUserInterface(_serviceProvider),
            Operation.DefineInterestRules => new InterestRuleInterface(_serviceProvider),
            Operation.PrintStatement => new BankStatementInterface(_serviceProvider),
            Operation.Quit => new LogoutUserInterface(),
            _ => new ErroMessageInterface()
        };

        return userInterface;
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
