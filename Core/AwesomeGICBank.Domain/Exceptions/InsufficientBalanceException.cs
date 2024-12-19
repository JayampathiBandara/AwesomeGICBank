namespace AwesomeGICBank.Domain.Exceptions;

public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(decimal amount)
    : base($"{amount} can't be widthrawn")
    {
    }
}