using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(AwesomeGICBankDbContext dbContext) : base(dbContext)
    {
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