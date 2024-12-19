using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.ValueObjects;

public class Transaction
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }

    public Transaction(string transactionId, TransactionType transactionType, decimal amount, DateOnly transactionDate)
    {
        if (string.IsNullOrEmpty(transactionId))
            throw new ArgumentNullException(nameof(transactionId));

        if (amount <= 0)
            throw new InvalidAmountException(amount, transactionType);

        Id = transactionId;
        Type = transactionType;
        Amount = amount;
        Date = transactionDate;
    }
}
