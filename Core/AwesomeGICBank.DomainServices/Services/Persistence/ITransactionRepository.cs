using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface ITransactionRepository
{
    Task CreateAsync(string accoutNo, Transaction transaction);
    Task CreateAsync(Account account, Transaction transaction);
    public Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate);
}

