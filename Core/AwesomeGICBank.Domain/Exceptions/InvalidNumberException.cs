namespace AwesomeGICBank.Domain.Exceptions;

public class InvalidNumberException : Exception
{
    public InvalidNumberException(string number)
    : base($"{number} is an invalid decimal number")
    {
    }
}