using AwesomeGICBank.Domain.DataTypes;

namespace AwesomeGICBank.Domain.Exceptions;

public class InvalidAmountException : Exception
{
    public InvalidAmountException(decimal amount, TransactionType transactionType)
    : base($"{amount} is an invalid {transactionType} amount")
    {
    }

}

public class InvalidRateException : Exception
{
    public InvalidRateException(decimal rate)
    : base($"{rate} is an invalid rate")
    {
    }

}