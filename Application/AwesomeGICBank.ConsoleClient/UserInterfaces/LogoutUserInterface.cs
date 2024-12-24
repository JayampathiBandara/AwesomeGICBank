namespace AwesomeGICBank.ConsoleClient.UserInterfaces;

public class LogoutUserInterface : IUserInterface
{
    public Task<bool> DisplayAsync()
    {
        Console.WriteLine("\n\nThank you for banking with AwesomeGIC Bank." +
            "\r\nHave a nice day!\n\n");
        return Task.FromResult(false);
    }
}
