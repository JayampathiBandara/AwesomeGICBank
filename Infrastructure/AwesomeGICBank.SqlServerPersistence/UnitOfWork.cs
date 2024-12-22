using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using AwesomeGICBank.SqlServerPersistence.Repositories;

namespace AwesomeGICBank.SqlServerPersistence;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly AwesomeGICBankDbContext _dbContext;

    public UnitOfWork(AwesomeGICBankDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    private IAccountRepository accountRepository;
    private IInterestRuleRepository interestRuleRepository;
    private ITransactionRepository transactionRepository;

    public IAccountRepository AccountRepository => accountRepository ??= new AccountRepository(_dbContext);
    public IInterestRuleRepository InterestRuleRepository => interestRuleRepository ??= new InterestRuleRepository(_dbContext);
    public ITransactionRepository TransactionRepository => transactionRepository ??= new TransactionRepository(_dbContext);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Dispose();

                accountRepository = null;
                interestRuleRepository = null;
                transactionRepository = null;
            }
            _disposed = true;
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_dbContext is not null)
        {
            _dbContext.ChangeTracker.Clear();
            await _dbContext.DisposeAsync().ConfigureAwait(false);
        }
    }
}