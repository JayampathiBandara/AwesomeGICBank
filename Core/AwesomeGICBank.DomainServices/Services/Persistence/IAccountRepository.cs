using AwesomeGICBank.Domain.Entities;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface IAccountRepository
{
    public Task<Account> CreateAsync(Account account);
    public Task<Account> GetAsync(string accountNo);
    public Task<Account> GetAsync(string accountNo, DateOnly TransactionDate);
}