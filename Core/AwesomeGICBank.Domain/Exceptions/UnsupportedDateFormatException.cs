namespace AwesomeGICBank.Domain.Exceptions;

public class UnsupportedDateFormatException : Exception
{
    public UnsupportedDateFormatException(string message)
    : base(message)
    {
    }
}