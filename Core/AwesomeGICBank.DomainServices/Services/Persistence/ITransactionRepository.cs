using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface ITransactionRepository
{
    public Task CreateAsync(string accoutNo, Transaction transaction);
    public Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate);
}

