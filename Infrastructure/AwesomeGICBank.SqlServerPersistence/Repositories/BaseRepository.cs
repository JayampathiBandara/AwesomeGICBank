using AwesomeGICBank.SqlServerPersistence.Configurations;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public abstract class BaseRepository
{
    protected readonly AwesomeGICBankDbContext _dbContext;
    public BaseRepository(AwesomeGICBankDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
