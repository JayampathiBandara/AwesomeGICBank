namespace AwesomeGICBank.Domain.Exceptions;

public class InvalidBankOperaionException : Exception
{
    public InvalidBankOperaionException()
    : base("Invalid bank operation exception. Key is not defined in operation list")
    {
    }
}
