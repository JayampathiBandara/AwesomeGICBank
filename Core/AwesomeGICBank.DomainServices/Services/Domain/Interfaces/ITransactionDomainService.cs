﻿using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Domain.Interfaces;

public interface ITransactionDomainService
{
    Task<TransactionId> GenerateNextTransactionId(DateOnly date);

    Task<Transaction> CreateTransactionAsync(string transactionType, string amount, string transactionDate);
}
