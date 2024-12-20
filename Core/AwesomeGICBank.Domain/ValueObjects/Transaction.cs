using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.ValueObjects;

public class Transaction
{
    public TransactionId Id { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }

    public Transaction(TransactionId id, TransactionType transactionType, decimal amount, DateOnly transactionDate)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));

        if (amount <= 0)
            throw new InvalidAmountException(amount, transactionType);

        Id = id;
        Type = transactionType;
        Amount = amount;
        Date = transactionDate;
    }
}
