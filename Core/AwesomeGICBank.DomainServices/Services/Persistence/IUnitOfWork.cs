namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{

    IAccountRepository AccountRepository { get; }
    IInterestRuleRepository InterestRuleRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    Task SaveAsync();
}

