using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public Task CreateAsync(string accoutNo, Domain.ValueObjects.Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate)
    {
        return transactionDate.ToString("yyyyMMdd") + "-01";
    }
}