namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface ITransactionRepository
{
    public Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate);
}

