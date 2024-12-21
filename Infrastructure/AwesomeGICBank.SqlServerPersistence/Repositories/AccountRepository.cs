using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task CreateAsync(Account account)
    {
        return Task.CompletedTask;
    }

    public Task<Account> GetAsync(string accountNo)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAsync(string accountNo, DateOnly TransactionDate)
    {
        throw new NotImplementedException();
    }
}
