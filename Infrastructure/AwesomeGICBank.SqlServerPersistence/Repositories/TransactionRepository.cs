using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public string GetMaximumTransactionNoAsync(DateOnly transactionDate)
    {
        return transactionDate.ToString("yyyyMMdd") + "-01";
    }
}
