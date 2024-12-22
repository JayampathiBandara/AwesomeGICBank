using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface IInterestRuleRepository
{
    public Task CreateOrUpdateAsync(InterestRule interestRule);
    public Task<List<InterestRule>> GetAllAsync();
    public Task<int> GetMaximumSequenceNoAsync(DateOnly transactionDate);
}

