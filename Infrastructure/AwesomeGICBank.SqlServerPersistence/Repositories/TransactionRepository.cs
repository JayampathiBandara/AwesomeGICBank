using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(AwesomeGICBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateAsync(string accoutNo, Transaction transaction)
    {
        var trackedAccount = _dbContext
            .Accounts
            .Local
            .FirstOrDefault(a => a.AccountNo == accoutNo);

        if (trackedAccount is not null)
        {
            trackedAccount.DoTransaction(transaction);
        }
        else
        {
            var existingAccount = await _dbContext
                .Accounts
                .FirstOrDefaultAsync(a => a.AccountNo == accoutNo);

            if (existingAccount is not null)
            {
                existingAccount.DoTransaction(transaction);
            }
        }
    }

    public async Task CreateAsync(Account account, Transaction transaction)
    {
        if (account is not null)
        {
            account.DoTransaction(transaction);
        }
    }

    public async Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate)
    {
        var transactions = await _dbContext.Transactions
            .Where(t => t.Date == transactionDate)
            .ToListAsync();

        var transactionId = transactions
            .OrderByDescending(t => t.Id.Value)
            .Select(t => t.Id.Value)
            .FirstOrDefault();

        return transactionId;
    }


}