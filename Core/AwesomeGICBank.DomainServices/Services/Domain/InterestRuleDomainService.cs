using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public class InterestRuleDomainService : IInterestRuleDomainService
{
    private readonly IInterestRuleRepository _interestRuleRepository;


    public InterestRuleDomainService(IInterestRuleRepository interestRuleRepository)
    {
        _interestRuleRepository = interestRuleRepository ?? throw new ArgumentNullException(nameof(interestRuleRepository));
    }

    public async Task<int> GenerateNextRuleSequenceNoAsync(DateOnly date)
    {
        var latestNumber = await _interestRuleRepository.GetMaximumSequenceNoAsync(date);

        return ++latestNumber;
    }
}