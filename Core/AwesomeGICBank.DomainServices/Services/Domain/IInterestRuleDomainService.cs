namespace AwesomeGICBank.DomainServices.Services.Domain;

public interface IInterestRuleDomainService
{
    Task<int> GenerateNextRuleSequenceNoAsync(DateOnly date);
}
