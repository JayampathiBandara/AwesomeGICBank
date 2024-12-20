namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface ITransactionRepository
{
    public string GetMaximumTransactionNoAsync(DateOnly transactionDate);
}

