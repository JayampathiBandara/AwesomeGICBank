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

    public async Task CreateAsync(Account account)
    {
        await _dbContext.Set<Account>().AddAsync(account);
    }

    public async Task<Account> GetAsync(string accountNo)
    {
        var account = await _dbContext
            .Set<Account>()
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.AccountNo == accountNo);

        return account;
    }

    public async Task<bool> ExistsAsync(string accountNo)
    {
        var account = await _dbContext
            .Set<Account>()
            .AnyAsync(x => x.AccountNo == accountNo);

        return account;
    }
}
