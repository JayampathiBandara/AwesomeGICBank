using AwesomeGICBank.Domain.DataTypes;
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

    public async Task<decimal> GetBalanceAsOfDateAsync(string accountNo, DateOnly toDate)
    {
        var balance = await _dbContext
            .Set<Transaction>()
            .Where(x => x.AccountNo == accountNo &&
                        x.Date < toDate)
            .Select(x => x.Type == TransactionType.Withdrawal ? -x.Amount : x.Amount)
            .SumAsync();

        return balance;
    }

    public async Task<List<Transaction>> GetAsync(string accountNo, DateOnly StartDate, DateOnly EndDate)
    {
        var transactions = await _dbContext
            .Set<Transaction>()
            .Where(x => x.AccountNo == accountNo &&
                        x.Date >= StartDate &&
                        x.Date <= EndDate)
            .ToListAsync();

        return transactions;
    }

    public async Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate)
    {
        var transactions = await _dbContext.Transactions
            .Where(t => t.Date == transactionDate)
            .AsNoTracking()
            .ToListAsync();

        var transactionId = transactions
            .OrderByDescending(t => t.TransactionId)
            .Select(t => t.TransactionId)
            .FirstOrDefault();

        return transactionId;
    }


}