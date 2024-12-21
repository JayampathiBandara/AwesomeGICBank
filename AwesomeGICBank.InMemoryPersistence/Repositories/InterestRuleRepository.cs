using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.InMemoryPersistence.Repositories;

public class InterestRuleRepository : BaseRepository, IInterestRuleRepository
{
    public InterestRuleRepository(Bank bank) : base(bank)
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