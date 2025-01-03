﻿using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.ValueObjects;
public class Transaction
{
    public TransactionId Id { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }

    public decimal SignedAmount =>
        Type switch
        {
            TransactionType.Deposit or TransactionType.Interest => Amount,
            TransactionType.Withdrawal => -Amount,
            _ => throw new NotImplementedException($"Undefined TransactionType: {Type}")
        };


    public Account Account { get; set; }
    public string AccountNo { get; set; }

    public string TransactionId => Id.Value;
    protected Transaction() { }
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

    /*public static decimal GetSignedAmount = (Transaction transaction) =>
         transaction.Type == TransactionType.Withdrawal ? -transaction.Amount : transaction.Amount;*/
}
