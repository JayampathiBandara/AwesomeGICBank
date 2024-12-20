using AwesomeGICBank.Domain.DataTypes;

namespace AwesomeGICBank.Domain.Exceptions;

public class InvalidAmountException : Exception
{
    public InvalidAmountException(string message)
        : base(message)
    {
    }

    public InvalidAmountException(decimal amount, TransactionType transactionType)
        : base($"{amount} is an invalid {transactionType} amount")
    {
    }
}
