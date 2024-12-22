using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.InMemoryPersistence.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(Bank bank) : base(bank)
    {

    }
    public async Task CreateAsync(Account account, Transaction transaction)
    {
        if (account is not null)
            account.DoTransaction(transaction);

    }
    public async Task CreateAsync(string accountNo, Transaction transaction)
    {
        var account = _bank.Accounts.Where(x => x.AccountNo == accountNo).FirstOrDefault();

        if (account == null)
            throw new ArgumentNullException(nameof(transaction));

        account.DoTransaction(transaction);
    }

    public Task<string> GetMaximumTransactionIdAsync(DateOnly transactionDate)
    {
        var transactionId = _bank.Accounts
            .SelectMany(x => x.Transactions)
            .Where(x => x.Date == transactionDate)
            .Max(x => x.Id.Value);

        return Task.FromResult(transactionId);
    }


}
