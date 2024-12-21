using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface IInterestRuleRepository
{
    public Task CreateAsync(InterestRule interestRule);
    public Task<int> GetMaximumSequenceNoAsync(DateOnly transactionDate);
}

