using AwesomeGICBank.Domain.DataTypes;

namespace AwesomeGICBank.Domain.Exceptions;

public class UnsupportedTransactionTypeException : Exception
{
    public UnsupportedTransactionTypeException(TransactionType transactionType)
    : base($"Unsupported transaction type exception. {transactionType}")
    {
    }
}
