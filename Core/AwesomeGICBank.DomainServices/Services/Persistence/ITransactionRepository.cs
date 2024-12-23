using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface ITransactionRepository
{
    public Task<List<Transaction>> GetAsync(string accountNo, DateOnly StartDate, DateOnly EndDate);
    public Task<decimal> GetBalanceAsOfDateAsync(string accountNo, DateOnly toDate);
    public Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate);

}

