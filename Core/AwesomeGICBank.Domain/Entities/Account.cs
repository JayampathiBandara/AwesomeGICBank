using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.Domain.Entities;

public class Account
{
    private readonly List<Transaction> _transactions;

    public string AccountNo { get; init; }

    public IReadOnlyList<Transaction> Transactions
    {
        get { return _transactions; }
    }

    public decimal Balance => _transactions
        .Sum(x =>
        {
            if (x.Type == TransactionType.Deposit)
                return x.Amount;
            else if (x.Type == TransactionType.Withdrawal)
                return -1 * x.Amount;
            else return 0;
        });

    protected Account() { }

    public Account(string accountNo)
    {
        if (string.IsNullOrEmpty(accountNo))
            throw new ArgumentNullException(nameof(accountNo));

        AccountNo = accountNo;
        _transactions = new();
    }

    public void DoTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        switch (transaction.Type)
        {
            case TransactionType.Deposit:
                Deposit(transaction);
                break;
            case TransactionType.Withdrawal:
                Withdraw(transaction);
                break;
            default:
                throw new UnsupportedTransactionTypeException(transaction.Type);
        }
    }

    private void Deposit(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    private void Withdraw(Transaction transaction)
    {
        if (Balance < transaction.Amount)
            throw new InsufficientBalanceException(transaction.Amount);

        _transactions.Add(transaction);
    }
}