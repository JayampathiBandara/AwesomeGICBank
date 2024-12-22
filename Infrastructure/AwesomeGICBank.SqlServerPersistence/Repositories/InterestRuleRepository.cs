using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class InterestRuleRepository :
  BaseRepository, IInterestRuleRepository
{
    public InterestRuleRepository(AwesomeGICBankDbContext dbContext) : base(dbContext)
    {
    }

    public Task CreateAsync(InterestRule interestRule)
    {
        return Task.CompletedTask;
    }

    public Task<int> GetMaximumSequenceNoAsync(DateOnly transactionDate)
    {
        return Task.FromResult(2);
    }
}