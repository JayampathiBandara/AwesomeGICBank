using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.InMemoryPersistence.Repositories;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(Bank bank) : base(bank)
    {

    }

    public Task CreateAsync(Account account)
    {
        if (!_bank.Accounts.Any(x => x.AccountNo.Equals(account.AccountNo)))
            _bank.Accounts.Add(account);

        return Task.CompletedTask;
    }

    public async Task<Account> GetAsync(string accountNo)
    {
        var account = _bank.Accounts
            .FirstOrDefault(x => x.AccountNo == accountNo);

        return account;
    }

    public async Task<Account> GetAsync(string accountNo, DateOnly transactionDate)
    {
        var account = _bank.Accounts
          .FirstOrDefault(x => x.AccountNo == accountNo);

        return account;
    }
}
