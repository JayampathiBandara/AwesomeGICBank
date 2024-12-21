using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class InterestRuleRepository : IInterestRuleRepository
{
    public Task CreateAsync(InterestRule interestRule)
    {
        return Task.CompletedTask;
    }

    public Task<int> GetMaximumSequenceNoAsync(DateOnly transactionDate)
    {
        return Task.FromResult(2);
    }
}