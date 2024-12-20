using AwesomeGICBank.Domain.Entities;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface IAccountRepository
{
    public Task CreateAsync(Account account);
}

public interface ITransactionRepository
{
    public string GetMaximumTransactionNo(DateOnly transactionDate);
}

