namespace AwesomeGICBank.ConsoleClient.UserInterfaces;

public class ErroMessageInterface : IUserInterface
{
    public Task<bool> DisplayAsync()
    {
        Console.WriteLine("\n\nInvalid Operation. Please Select Correct Operation.\n\n");
        return Task.FromResult(true);
    }
}
