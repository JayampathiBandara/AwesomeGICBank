using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(AwesomeGICBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Account> CreateAsync(Account account)
    {
        if (!_dbContext.Accounts.Any(x => x.AccountNo == account.AccountNo))
        {
            await _dbContext.Set<Account>().AddAsync(account);
        }
        return account;
    }

    public async Task<Account> GetAsync(string accountNo)
    {
        var account = await _dbContext
            .Set<Account>()
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.AccountNo == accountNo);

        return account;
    }

    public async Task<Account> GetAsync(string accountNo, DateOnly TransactionDate)
    {
        throw new NotImplementedException();
    }
}
