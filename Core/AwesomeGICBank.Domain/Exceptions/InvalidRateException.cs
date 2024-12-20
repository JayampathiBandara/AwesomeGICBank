namespace AwesomeGICBank.Domain.Exceptions;

public class InvalidRateException : Exception
{
    public InvalidRateException(decimal rate)
    : base($"{rate} is an invalid rate")
    {
    }

}